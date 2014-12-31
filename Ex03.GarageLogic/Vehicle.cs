using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_ModelName;
        protected string m_LicenseNumber;
        protected float m_RemainingEnergyPercentage;
        protected List<Tire> m_Tires;
        protected readonly int r_NumOfTires;
        protected string m_OwnerName;
        protected string m_OwnerPhone;
        protected eVehicleStatus m_VehicleStatus = eVehicleStatus.InReparation;
        protected readonly eTypeOfEnergy r_TypeOfEnergy;
        protected readonly GenericEnergySystem r_EnergySystem;


        public Vehicle(eTypeOfEnergy i_TypeOfEnergy, int i_NumOfTires, float i_MaxAirPressure)
        {
            r_TypeOfEnergy = i_TypeOfEnergy;
            r_NumOfTires = i_NumOfTires;
            m_Tires = new List<Vehicle.Tire>();
            //for (int i = 0; i < r_NumOfTires; i++)
            //{
            //    m_Tires.Add(new Tire(29, i_TireBrandName)); LETAKEN PO
            //}
        }

        public Vehicle(eTypeOfEnergy i_TypeOfEnergy, eFuelType i_FuelType, float i_FuelTankVolume, int i_NumOfTires, float i_MaxAirPressure) : this(i_TypeOfEnergy, i_NumOfTires, i_MaxAirPressure)
        {
            if(i_TypeOfEnergy == eTypeOfEnergy.Fuel)
            {
                r_EnergySystem = new FuelSystem(i_FuelTankVolume, i_FuelType);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public Vehicle(eTypeOfEnergy i_TypeOfEnergy, float i_MaxBatteryTime, int i_NumOfTires, float i_MaxAirPressure) : this(i_TypeOfEnergy, i_NumOfTires, i_MaxAirPressure)
        {
            if (i_TypeOfEnergy == eTypeOfEnergy.Electric)
            {
                r_EnergySystem = new ElectricSystem(i_MaxBatteryTime);
                r_TypeOfEnergy = eTypeOfEnergy.Electric;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public Vehicle(string i_ModelName, string i_LicenseNumber, eTypeOfEnergy i_TypeOfEnergy, eFuelType i_FuelType, float i_TankVolume)
        {
            if (i_TypeOfEnergy == eTypeOfEnergy.Fuel)
            {
                r_EnergySystem = new FuelSystem(i_TankVolume, i_FuelType);
                r_TypeOfEnergy = eTypeOfEnergy.Fuel;
            }
            else
            {
                throw new ArgumentException();
            }
            m_ModelName = i_ModelName;
            m_LicenseNumber = i_LicenseNumber;
        }
        
        public List<Tire> Tires
        {
            get { return m_Tires; }
            set { m_Tires = value; }
        }

        public string OwnerName
        {
            get { return m_OwnerName; }
            set { m_OwnerName = value; }
        }



        public string OwnerPhone
        {
            get { return m_OwnerPhone; }
            set { m_OwnerPhone = value; }
        }
        

        public class Tire
        {
            private string m_BrandName;
            private float m_AirPressure;
            private readonly float r_MaxAirPressure;

            public Tire(float i_CurrAirPressure, float i_MaxAirPressure, string i_TireBrandName)
            {
                r_MaxAirPressure = i_MaxAirPressure;
                m_AirPressure = i_CurrAirPressure;
                m_BrandName = i_TireBrandName;
            }

            public float MaxAirPressure
            {
                get { return r_MaxAirPressure; }
            }

            public float AirPressure
            {
                get { return m_AirPressure; }
            }

            public string BrandName
            {
                get { return m_BrandName; }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="i_AirToAdd"></param>
            public void Inflate(float i_AirToAdd)
            {
                if (m_AirPressure + i_AirToAdd <= r_MaxAirPressure)
                {
                    m_AirPressure += i_AirToAdd;
                }
                else
                {
                    throw new ValueOutOfRangeException(0,r_MaxAirPressure);
                }                
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string Print()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("Air pressure: ").Append(m_AirPressure).
                              Append(" Brand name: ").Append(m_BrandName).Append(Environment.NewLine);

                return stringBuilder.ToString();
            }
        }

        public abstract class GenericEnergySystem
        {
            public abstract string Print();
        }

        public class FuelSystem : GenericEnergySystem
        {
            private readonly eFuelType r_FuelType;
            private float m_CurrFuelQuantity;
            private readonly float r_FuelTankVolume;

      

            public FuelSystem(float i_FuelTankVolume, eFuelType i_FuelType)
            {
                r_FuelType = i_FuelType;
                r_FuelTankVolume = i_FuelTankVolume;
            }

            public FuelSystem()
            {
                // TODO: Complete member initialization
            }
            
            public void AddFuel(float i_FuelLitersToAdd, eFuelType i_FuelTypeToAdd)
            {
                if(i_FuelTypeToAdd != r_FuelType)
                {
                    throw new ArgumentException(String.Format("Fuel {0} is not compatible with this vehicle, it uses {1}",
                        i_FuelTypeToAdd, r_FuelType));
                }
                if (m_CurrFuelQuantity + i_FuelLitersToAdd <= r_FuelTankVolume)
                {
                    m_CurrFuelQuantity += i_FuelLitersToAdd;
                }
                else
                {
                    throw new ValueOutOfRangeException("Error: You tried to add too much fuel.");
                }
            }

            public override string Print()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Fuel engine").Append(Environment.NewLine);
                sb.Append("Fuel type: ").Append(Enum.GetName(typeof(eFuelType), r_FuelType)).Append(Environment.NewLine);
                sb.Append("Current fuel volume: ").Append(m_CurrFuelQuantity).Append(Environment.NewLine);
                sb.Append("Max fuel volume: ").Append(r_FuelTankVolume).Append(Environment.NewLine);

                return sb.ToString();
            }
        }

        public class ElectricSystem : GenericEnergySystem
        {
            private float m_RemainingBatteryTime;
            private readonly float r_MaxBatteryTime;

            public ElectricSystem(float i_MaxBatteryTime)
            {
                r_MaxBatteryTime = i_MaxBatteryTime;
            }

            public void ChargeBattery(float i_BatteryTimeToAdd)
            {
                if(m_RemainingBatteryTime + i_BatteryTimeToAdd <= r_MaxBatteryTime)
                {
                    m_RemainingBatteryTime += i_BatteryTimeToAdd;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, r_MaxBatteryTime);
                }
            }

            public override string Print()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Electric engine").Append(Environment.NewLine);
                sb.Append("Remaining battery time: ").Append(m_RemainingBatteryTime).Append(Environment.NewLine);
                sb.Append("Max battery time: ").Append(r_MaxBatteryTime).Append(Environment.NewLine);

                return sb.ToString();
            }

                
        }

        public eVehicleStatus VehicleStatus
        {
            get { return m_VehicleStatus; }
            set { m_VehicleStatus = value; }
        }

        public eTypeOfEnergy TypeOfEnergy
        {
            get { return r_TypeOfEnergy; }
        }

        public GenericEnergySystem GetEnergySystem()
        {
            return r_EnergySystem;
        }

        public string LicenseNum
        {
            get {return m_LicenseNumber;}
            set { m_LicenseNumber = value; }
        }


        public void InflateTires()
        {
            foreach (Tire tire in m_Tires)
            {
                tire.Inflate(tire.MaxAirPressure - tire.AirPressure);
            }
        }

        public string GetTireDetails()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Tire tire in m_Tires)
            {
                stringBuilder.Append(tire.Print());
            }
            return stringBuilder.ToString();
        }

        public virtual string Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Brand name: ").Append(m_ModelName).Append(Environment.NewLine).
               Append("License numbers: ").Append(m_LicenseNumber).Append(Environment.NewLine).
               Append("Owner name: ").Append(m_OwnerName).Append(Environment.NewLine).
               Append("Owner phone: ").Append(m_OwnerPhone).Append(Environment.NewLine).
               Append("Status: ").Append(m_VehicleStatus).Append(Environment.NewLine).
               Append("Tires: ").Append(Environment.NewLine).Append(GetTireDetails()).Append(Environment.NewLine).
               Append("Engine: ").Append(Environment.NewLine).Append(r_EnergySystem.Print()).Append(Environment.NewLine); 
        
            return sb.ToString();
        }
    }
}
