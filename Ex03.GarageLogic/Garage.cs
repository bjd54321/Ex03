using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        List<Vehicle> m_FuelVehicles;
        List<Vehicle> m_ElectricVehicles;
        VehicleBuilder m_VehicleBuilder;

        public Garage()
        {
            m_FuelVehicles = new List<Vehicle>();
            m_ElectricVehicles = new List<Vehicle>();
            m_VehicleBuilder = new VehicleBuilder();
        }

        public List<Vehicle> GetFuelVehicles
        {
            get { return m_FuelVehicles; }
        }

        public List<Vehicle> GetElectricVehicles
        {
            get { return m_ElectricVehicles;  }
        }

        public VehicleBuilder vehicleBuilder
        {
            get { return m_VehicleBuilder; }
        }
    }
}
