using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Car : Vehicle
    {
        private eCarColor m_CarColor;
        private eNumOfDoors m_NumOfDoors = 4;



        public eNumOfDoors NumOfDoors
        {
            get { return m_NumOfDoors; }
            set { m_NumOfDoors = value; }
        }
        

        public Car(string i_TireBrandName)
        {
            m_NumOfTires = 4;
            m_Tires = new List<Vehicle.Tire>();
            for (int i = 0; i < m_NumOfTires; i++)
            {
                m_Tires.Add(new Tire(29, i_TireBrandName));
            }
        }
        
        public Car(eFuelType i_FuelType, float i_FuelTankVolume, string i_TireBrandName) : this(i_TireBrandName)
        {
            r_TypeOfEnergy = eTypeOfEnergy.Fuel;
            m_EnergySystem = new FuelSystem(i_FuelTankVolume, i_FuelType);
        }

        public Car(eTypeOfEnergy i_TypeOfEnergy, float i_MaxBatteryTime, string i_TireBrandName) : this(i_TireBrandName)
        {
            r_TypeOfEnergy = i_TypeOfEnergy;
            m_EnergySystem = new ElectricSystem(i_MaxBatteryTime);
        }

        public override string Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.Print());
            sb.Append("Color: ").Append(m_CarColor).Append(Environment.NewLine);
            sb.Append("Number of doors: ").Append(m_NumOfDoors).Append(Environment.NewLine);

            return sb.ToString();
        }
    }
}
