using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Truck : Vehicle
    {
        private bool m_CarriesDangerousMaterials;
        private readonly float r_MaxCarryWeight;
        private float m_CarryWeight;

        public Truck() : base(eTypeOfEnergy.Fuel)
        {

        }
    }
}
