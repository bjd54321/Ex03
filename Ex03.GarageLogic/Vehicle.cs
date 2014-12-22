using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Vehicle
    {
        protected string m_BrandName;
        protected string m_LicenseNum;
        protected float m_RemainingEnergyPercentage;
        protected List<Tire> m_TireCollection;
        protected int m_NumOfTires;
        protected string m_OwnerName;
        protected string m_OwnerPhone;
        protected eVehicleStatus m_VehicleStatus = eVehicleStatus.InReparation;
        protected eTypeOfEnergy r_TypeOfEnergy;
        protected EnergySystem m_EnergySystem;

        protected class Tire
        {
            private string m_BrandName;
            private float m_AirPressure;
            private readonly float r_MaxAirPressure;

            public Tire(float i_MaxAirPressure, string i_TireBrandName)
            {
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
        }

        public abstract class EnergySystem
        {

        }

        public class FuelSystem : EnergySystem
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
                    throw new ArgumentException();
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
        }

        public class ElectricSystem : EnergySystem
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

        public string LicenseNum
        {
            get {return m_LicenseNum;}
            set { m_LicenseNum = value; }
        }


        public void inflateTires()
        {
            foreach (Tire tire in m_TireCollection)
            {
                tire.Inflate(tire.MaxAirPressure);
            }
        }

        public string getTireDetails()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Tire tire in m_TireCollection)
            {
                stringBuilder.Append("Air pressure: " + tire.AirPressure);
                stringBuilder.Append("Brand name: " + tire.BrandName);
            }
            return stringBuilder.ToString();
        }

        public string getAdditionalDetails()
        {
            // This method should return the fuel status and type of fuel / charge status / other vehicle specific details
            throw new NotImplementedException();
        }
    }
}
