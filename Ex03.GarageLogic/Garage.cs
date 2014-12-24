using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        // Key is licence number in both 
        Dictionary<string, Vehicle> m_FuelVehicles;
        Dictionary<string, Vehicle> m_ElectricVehicles;
        VehicleBuilder m_VehicleBuilder;

        public Garage()
        {
            m_FuelVehicles = new Dictionary<string, Vehicle>();
            m_ElectricVehicles = new Dictionary<string, Vehicle>();
            m_VehicleBuilder = new VehicleBuilder();
        }

        public Dictionary<string, Vehicle> GetFuelVehicles
        {
            get { return m_FuelVehicles; }
        }

        public Dictionary<string, Vehicle> GetElectricVehicles
        {
            get { return m_ElectricVehicles;  }
        }

        public VehicleBuilder vehicleBuilder
        {
            get { return m_VehicleBuilder; }
        }

        public void AddVehicle(Vehicle vehicle)
        {
            if (vehicle != null)
            {
                if (eTypeOfEnergy.Electric.Equals(vehicle.TypeOfEnergy))
                {
                    m_ElectricVehicles.Add(vehicle.LicenseNum, vehicle);
                }
                else if (eTypeOfEnergy.Fuel.Equals(vehicle.TypeOfEnergy))
                {
                    m_FuelVehicles.Add(vehicle.LicenseNum, vehicle);
                }
                else
                {
                    // Unsupported type
                }
            }
        }

        public Vehicle GetVehicle(string licenseNumber)
        {
            Vehicle vehicle = null;
            if (licenseNumber != null)
            {
                // Try to find in fuel vehicles
                // If not, try to find in electric vehicles
                if (!m_FuelVehicles.TryGetValue(licenseNumber, out vehicle))
                {
                    m_ElectricVehicles.TryGetValue(licenseNumber, out vehicle);
                }
            }

            return vehicle;
        }

        public void Inflate(string licenceNumber)
        {
            Vehicle vehicle = GetVehicle(licenceNumber);
            if (vehicle != null)
            {
                vehicle.InflateTires();
            }
        }

        public void AddFuel(Vehicle fuelVehicle, float i_FuelLitersToAdd, eFuelType i_FuelTypeToAdd)
        {
            if (fuelVehicle.GetEnergySystem().GetType() == typeof(Vehicle.FuelSystem))
            {
                ((Vehicle.FuelSystem)fuelVehicle.GetEnergySystem()).AddFuel(i_FuelLitersToAdd, i_FuelTypeToAdd);
            }
        }

        public void Charge(Vehicle electricVehicle, float chargeAmount)
        {
            throw new NotImplementedException();
        }

        public void ChangeStatus(Vehicle vehicle, eVehicleStatus status)
        {
            throw new NotImplementedException();
        }
    }
}
