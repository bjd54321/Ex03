using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleBuilder
    {
        public enum eTypeOfVehicle
        {
            FuelCar,
            ElectricCar,
            FuelMotorcicle,
            ElectricMotorcicle,
            Truck,
        }

        public Vehicle buildVehicle(eTypeOfVehicle i_TypeOfVehicle)
        {
            Vehicle vehicle = null;
            switch (i_TypeOfVehicle)
            {
                case eTypeOfVehicle.FuelCar:
                    vehicle = new Car(eFuelType.Octan95, 45, "");
                    break;
                case eTypeOfVehicle.ElectricCar:
                    vehicle = new Car(eTypeOfEnergy.Electric, 2.6f, "");
                    break;
                case eTypeOfVehicle.FuelMotorcicle:
                    vehicle = new Motorcicle(eTypeOfEnergy.Fuel);
                    break;
                case eTypeOfVehicle.ElectricMotorcicle:
                    vehicle = new Motorcicle(eTypeOfEnergy.Electric);
                    break;
                case eTypeOfVehicle.Truck:
                    vehicle = new Truck();
                    break;
                default:
                    break;
            }
            return vehicle;
        }
    }
}
