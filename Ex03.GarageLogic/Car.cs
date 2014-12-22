using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Car : Vehicle
    {
        private eCarColor m_CarColor;
        private readonly eNumOfDoors r_NumOfDoors;
        

        public Car(string i_TireBrandName)
        {
            m_NumOfTires = 4;
            m_TireCollection = new List<Vehicle.Tire>();
            for (int i = 0; i < m_NumOfTires; i++)
            {
                m_TireCollection.Add(new Tire(29, i_TireBrandName));
            }
        }
        
        public Car(eFuelType i_FuelType, float i_FuelTankVolume, string i_TireBrandName) : this(i_TireBrandName)
        {
            r_TypeOfEnergy = eTypeOfEnergy.Fuel;
            m_EnergySystem = new FuelSystem(i_FuelTankVolume);
        }

        public Car(eTypeOfEnergy i_TypeOfEnergy, float i_MaxBatteryTime, string i_TireBrandName) : this(i_TireBrandName)
        {
            r_TypeOfEnergy = i_TypeOfEnergy;
            m_EnergySystem = new ElectricSystem(i_MaxBatteryTime);
        }
    }
}
