using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Vehicle
    {
        private string m_BrandName;
        private string m_LicenseNum;
        private float m_RemainingEnergyPercentage;
        private List<Tire> m_TireCollection;
        private string m_OwnerName;
        private string m_OwnerPhone;
        private eVehicleStatus m_VehicleStatus = eVehicleStatus.InReparation;
        private eTypeOfEnergy r_TypeOfEnergy;

        private class Tire
        {
            private string m_BrandName;
            private float m_AirPressure;
            private readonly float r_MaxAirPressure;

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

        public class EnergySystem
        {

        }

        public class FuelSystem : EnergySystem
        {
            private eFuelType m_FuelType;
            private float m_CurrFuelQuantity;
            private readonly float r_MaxFuelQuantity;
            
            public void AddFuel(float i_FuelLitersToAdd, eFuelType i_FuelTypeToAdd)
            {
                if(i_FuelTypeToAdd != m_FuelType)
                {
                    throw new ArgumentException();
                }
                if (m_CurrFuelQuantity + i_FuelLitersToAdd <= r_MaxFuelQuantity)
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

        public string LicenseNum
        {
            get {return m_LicenseNum;}
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
