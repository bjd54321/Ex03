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

        public Truck() 
        {

        }

        public override string Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.Print());
            sb.Append("Carries dangerous materials? ").Append(m_CarriesDangerousMaterials).Append(Environment.NewLine);
            sb.Append("Max carry weight: ").Append(r_MaxCarryWeight).Append(Environment.NewLine);
            sb.Append("Carry weight: ").Append(m_CarryWeight).Append(Environment.NewLine);

            return sb.ToString();
        }
    }
}
