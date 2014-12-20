using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcicle : Vehicle
    {
        private eLicenseType m_LicenseType;
        private readonly int r_EngineVolume; // in cm^3

        public Motorcicle(eTypeOfEnergy i_TypeOfEnergy) : base(i_TypeOfEnergy)
        {
            m_NumOfTires = 2;
        }
    }
}
