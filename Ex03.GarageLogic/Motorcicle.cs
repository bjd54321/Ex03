using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcicle : Vehicle
    {
        private eLicenseType m_LicenseType;
        private int r_EngineVolume; // in cm^3


        public Motorcicle(eFuelType i_FuelType, float i_FuelTankVolume, int i_MaxAirPressure)
        {
            r_TypeOfEnergy = eTypeOfEnergy.Fuel;
            m_EnergySystem = new FuelSystem(i_FuelTankVolume, i_FuelType);
            m_NumOfTires = 2;
            InitTires("", i_MaxAirPressure);
        }

        public Motorcicle(eTypeOfEnergy i_TypeOfEnergy, float i_MaxBatteryTime, int i_MaxAirPressure)
        {
            r_TypeOfEnergy = i_TypeOfEnergy;
            m_EnergySystem = new ElectricSystem(i_MaxBatteryTime);
            m_NumOfTires = 2;
            InitTires("", i_MaxAirPressure);
        }

        public eLicenseType LicenseType 
        { 
            get { return m_LicenseType; }
            set { m_LicenseType = value; } 
        }

        public int EngineVolume
        {
            get { return r_EngineVolume; }
            set { r_EngineVolume = value; }
        }

        public override string Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.Print());
            sb.Append("License type: ").Append(m_LicenseType).Append(Environment.NewLine);
            sb.Append("Engine volume: ").Append(r_EngineVolume).Append(Environment.NewLine);

            return sb.ToString();
        }
    }
}
