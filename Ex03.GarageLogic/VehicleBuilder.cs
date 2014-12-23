using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
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
    }
}
