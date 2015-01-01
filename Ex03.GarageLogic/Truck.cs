using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Truck : Vehicle
    {
        private bool m_CarriesDangerousMaterials;
        private float m_MaxCarryWeight;
        private float m_CarryWeight;



        public bool CarriesDangerousMaterials
        {
            get { return m_CarriesDangerousMaterials; }
            set { m_CarriesDangerousMaterials = value; }
        }


        public float CarryWeight
        {
            get { return m_CarryWeight; }
            set { m_CarryWeight = value; }
        }


        public float MaxCarryWeight
        {
            get { return m_MaxCarryWeight; }
            set 
            { 
                if (value < m_CarryWeight)
                {
                    throw new ValueOutOfRangeException(String.Format("Your truck is already carries {0} tonn", m_CarryWeight));
                }
                else
                {
                    m_MaxCarryWeight = value; 
                }                
            }
        }



        public Truck() 
        {          
            m_NumOfTires = 8;
            m_EnergySystem = new FuelSystem(200, eFuelType.Soler);
            r_TypeOfEnergy = eTypeOfEnergy.Fuel;
            InitTires("", 24);
        }

        public override string Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.Print());
            sb.Append("Carries dangerous materials? ").Append(m_CarriesDangerousMaterials).Append(Environment.NewLine);
            sb.Append("Max carry weight: ").Append(m_MaxCarryWeight).Append(Environment.NewLine);
            sb.Append("Carry weight: ").Append(m_CarryWeight).Append(Environment.NewLine);

            return sb.ToString();
        }
    }
}
