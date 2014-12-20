using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        List<Vehicle> m_VehiclesList;
        VehicleBuilder m_VehicleBuilder;

        public Garage()
        {
            m_VehiclesList = new List<Vehicle>();
            m_VehicleBuilder = new VehicleBuilder();
        }

        public List<Vehicle> VehiclesList
        {
            get { return m_VehiclesList; }
        }

        public VehicleBuilder vehicleBuilder
        {
            get { return m_VehicleBuilder; }
        }
    }
}
