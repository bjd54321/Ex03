using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_BrandName;
        protected string m_LicenseNum;
        protected float m_RemainingEnergyPercentage;
        protected List<Tire> m_Tires;
        protected int m_NumOfTires;
        protected string m_OwnerName;
        protected string m_OwnerPhone;
        protected eVehicleStatus m_VehicleStatus = eVehicleStatus.InReparation;
        protected eTypeOfEnergy r_TypeOfEnergy;
        protected GenericEnergySystem m_EnergySystem;

        protected class Tire
        {
            private string m_BrandName;
            private float m_AirPressure;
            private readonly float r_MaxAirPressure;

            public Tire(float i_MaxAirPressure, string i_TireBrandName)
            {
                r_MaxAirPressure = i_MaxAirPressure;
                m_AirPressure = i_MaxAirPressure;
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
            private eFuelType m_FuelType;
            private float m_CurrFuelQuantity;
            private readonly float r_FuelTankVolume;

            public FuelSystem(float i_FuelTankVolume)
            {
                r_FuelTankVolume = i_FuelTankVolume;
            }
            
            public void AddFuel(float i_FuelLitersToAdd, eFuelType i_FuelTypeToAdd)
            {
                if(i_FuelTypeToAdd != m_FuelType)
                {
                    throw new ArgumentException(String.Format("Fuel {0} is not compatible with this vehicle, it uses {1}",
                        i_FuelTypeToAdd, m_FuelType));
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
                sb.Append("Fuel type: ").Append(m_FuelType).Append(Environment.NewLine);
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
            return m_EnergySystem;
        }

        public string LicenseNum
        {
            get {return m_LicenseNum;}
            set { m_LicenseNum = value; }
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
            sb.Append("Brand name: ").Append(m_BrandName).Append(Environment.NewLine).
               Append("License numbers: ").Append(m_LicenseNum).Append(Environment.NewLine).
               Append("Owner name: ").Append(m_OwnerName).Append(Environment.NewLine).
               Append("Owner phone: ").Append(m_OwnerPhone).Append(Environment.NewLine).
               Append("Status: ").Append(m_VehicleStatus).Append(Environment.NewLine).
               Append("Tires: ").Append(Environment.NewLine).Append(GetTireDetails()).Append(Environment.NewLine).
               Append("Engine: ").Append(Environment.NewLine).Append(m_EnergySystem.Print()).Append(Environment.NewLine); 
        
            return sb.ToString();
        }
    }
}
