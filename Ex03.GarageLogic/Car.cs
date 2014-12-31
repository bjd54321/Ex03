using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Car : Vehicle
    {
        private eCarColor m_CarColor;
        private eNumOfDoors m_NumOfDoors = eNumOfDoors.Four;
        private string modelName;
        private string licenseNumber;
        private eCarColor color;
        private int numOfDoors;
        private eTypeOfEnergy eTypeOfEnergy;
        private eFuelType fuelType;
        private float currFuelAmount;
        private float tankVolume;
        


        public eNumOfDoors NumOfDoors
        {
            get { return m_NumOfDoors; }
            set { m_NumOfDoors = value; }
        }


        //public Car(eTypeOfEnergy i_TypeOfEnergy, string[] i_Details)
        //{
        //    //Vehicle(i_TypeOfEnergy, i_FuelType, i_FuelTankVolume, i_NumOfTires, i_MaxAirPressure)
        //}
        
        public Car(eTypeOfEnergy i_TypeOfEnergy, eFuelType i_FuelType, float i_FuelTankVolume, int i_NumOfTires, float i_MaxAirPressure) : base(i_TypeOfEnergy, i_FuelType, i_FuelTankVolume, i_NumOfTires, i_MaxAirPressure)
        {
        }

        public Car(eTypeOfEnergy i_TypeOfEnergy, float i_MaxBatteryTime, int i_NumOfTires, float i_MaxAirPressure) : base(i_TypeOfEnergy, i_MaxBatteryTime, i_NumOfTires, i_MaxAirPressure)
        {
        }

        public Car(string i_ModelName, string i_LicenseNumber, eCarColor i_Color, int i_NumOfDoors, eTypeOfEnergy i_TypeOfEnergy, eFuelType i_FuelType, float i_CurrFuelAmount, float i_TankVolume): base(i_ModelName, i_LicenseNumber, i_TypeOfEnergy,i_FuelType, i_TankVolume)
        {
            m_CarColor = i_Color;
            m_NumOfDoors = (eNumOfDoors) i_NumOfDoors;

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
