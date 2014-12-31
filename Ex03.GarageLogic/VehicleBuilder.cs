using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    /// <summary>
    /// Factory method for creating predefined vehicles dynamically
    /// </summary>
    public class VehicleBuilder
    {
     
        public Vehicle buildVehicle(eVehicleType i_TypeOfVehicle)
        {
            Vehicle vehicle = null;
            switch (i_TypeOfVehicle)
            {
                case eVehicleType.FuelCar:
                    vehicle = new Car(eFuelType.Octan95, 45, "");
                    break;
                case eVehicleType.ElectricCar:
                    vehicle = new Car(eTypeOfEnergy.Electric, 2.6f, "");
                    break;
                case eVehicleType.FuelMotorcicle:
                    vehicle = new Motorcicle(eTypeOfEnergy.Fuel);
                    break;
                case eVehicleType.ElectricMotorcicle:
                    vehicle = new Motorcicle(eTypeOfEnergy.Electric);
                    break;
                case eVehicleType.Truck:
                    vehicle = new Truck();
                    break;
                default:
                    break;
            }
            return vehicle;
        }

        public enum eVehicleType
        {
            FuelCar = 1,
            ElectricCar,
            FuelMotorcicle,
            ElectricMotorcicle,
            Truck
        }

        internal string[] getVehicleTypesAsStrings()
        {
            List<String> types = new List<String>();
            types.Add("Fuel car");
            types.Add("Electric car");
            types.Add("Fuel motorcicle");
            types.Add("Electric motorcicle");
            types.Add("Truck");
            return types.ToArray();
        }
    }
}
