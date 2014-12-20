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

        private class Tire
        {
            private string m_BrandName;
            private float m_AirPressure;
            private readonly float r_MaxAirPressure;


            public void Inflate(float i_AirToAdd)
            {
                
            }
        }


    }
}
