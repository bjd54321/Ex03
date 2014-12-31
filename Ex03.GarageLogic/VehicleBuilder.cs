using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleBuilder
    {

        private readonly int r_MinVehicleType = 1;
        private readonly int r_MaxVehicleType = 5;
        string[] m_VehicleGeneralDetails = new string[Enum.GetNames(typeof(eVehicleGeneralDetails)).Length];
        string[] m_FuelCarDetails = new string[Enum.GetNames(typeof(eFuelCarDetails)).Length];
        
        string[] m_ElectricCarDetails = { "color", "number of doors", "remaining battery time"};
        string[] m_FuelMotorcicleDetails = { "license type", "engine volume", "fuel type", "current fuel amount" };
        string[] m_ElectricMotorcicleDetails = { "license type", "engine volume", "remaining battery time"};
        string[] m_TruckDetails = { "does it carry dangerous materials", "maximum carry weight", "current carry weight", "fuel type", "current fuel amount" };
        

        public VehicleBuilder()
        {
            m_VehicleGeneralDetails[(int)eVehicleGeneralDetails.ModelName]="model name";
            m_VehicleGeneralDetails[(int)eVehicleGeneralDetails.LicenseNumber] = "license number";
            m_FuelCarDetails[(int)eFuelCarDetails.Color] = "color";
            m_FuelCarDetails[(int)eFuelCarDetails.CurrFuelAmount] = "current fuel amount";
            m_FuelCarDetails[(int)eFuelCarDetails.FuelType] = "fuel type";
            m_FuelCarDetails[(int)eFuelCarDetails.NumberOfDoors] = "number of doors";
        }

        public enum eVehicleGeneralDetails
        {
            ModelName,
            LicenseNumber
        }

        public enum eFuelCarDetails
        {
            Color,
            NumberOfDoors,
            FuelType,
            CurrFuelAmount
        }

        public string[] VehicleGeneralDetails
        {
            get { return m_VehicleGeneralDetails; }
        }

        public string[] GetVehicleDetails(eVehicleType i_TypeOfVehicle)
        {
            string[] vehicleDetails = {""};
            switch (i_TypeOfVehicle)
            {
                case eVehicleType.FuelCar:
                    vehicleDetails = m_FuelCarDetails;
                    break;
                case eVehicleType.ElectricCar:
                    vehicleDetails = m_ElectricCarDetails;
                    break;
                case eVehicleType.FuelMotorcicle:
                    vehicleDetails = m_FuelMotorcicleDetails;
                    break;
                case eVehicleType.ElectricMotorcicle:
                    vehicleDetails = m_ElectricMotorcicleDetails;
                    break;
                case eVehicleType.Truck:
                    vehicleDetails = m_TruckDetails;
                    break;
                default:
                    break;
            }

            return vehicleDetails;
        }

        public Vehicle buildVehicle(string i_VehicleLicenseNumber, eVehicleType i_TypeOfVehicle, string i_VehicleModel, string[] i_SpecificDetails)
        {
            //eFuelType fuelType;
            //float tankVolume;
            //int numOfTires;
            //float batteryTime;
            //bool carriesDangerousMaterials;
            //eCarColor color;
            //int numOfDoors;
            //eLicenseType licenseType;
            //int engineVolume;
            //float maxCarryWeight;
            //float currCarryWeight;
            //eFuelType fuelType;
            //float currFuelAmount;
            //float maxFuelAmount;
            //float remainingBatteryTime;
            //float maxBatteryTime;
            Vehicle vehicle = null;
            switch (i_TypeOfVehicle)
            {
                case eVehicleType.FuelCar:
         //           vehicle = new Car(eTypeOfEnergy.Fuel, eFuelType.Octan95, 45, 4, 29);
                    vehicle = buildFuelCar(i_VehicleLicenseNumber, i_VehicleModel, i_SpecificDetails);
                    //vehicle = new Car(eTypeOfEnergy.Fuel, i_GeneralDetails, i_SpecificDetails);
                    break;
                //case eVehicleType.ElectricCar:
                //    //vehicle = new Car(eTypeOfEnergy.Electric, 2.6f, 4, 29);
                //    vehicle = buildElectricCar(i_VehicleModel, i_SpecificDetails);
                //    break;
                //case eVehicleType.FuelMotorcicle:
                //    //vehicle = new Motorcicle(eTypeOfEnergy.Fuel, eFuelType.Octan96, 6.5f, 2, 30);
                //    vehicle = buildFuelMotorcicle(i_VehicleModel, i_SpecificDetails);
                //    break;
                //case eVehicleType.ElectricMotorcicle:
                //    //vehicle = new Motorcicle(eTypeOfEnergy.Electric, 1.8f, 2, 30);
                //    vehicle = buildElectricMotorcicle(i_VehicleModel, i_SpecificDetails);
                //    break;
                //case eVehicleType.Truck:
                //    //vehicle = new Truck(eTypeOfEnergy.Fuel, eFuelType.Octan95, 45, 8, 24);
                //    vehicle = buildTruck(i_VehicleModel, i_SpecificDetails);
                //    break;
                default:
                    break;
            }
            return vehicle;
        }

        private Vehicle buildTruck(string i_VehicleLicenseNumber, string i_VehicleModel, string[] i_SpecificDetails)
        {
            throw new NotImplementedException();
        }

        private Vehicle buildElectricMotorcicle(string i_VehicleLicenseNumber, string i_VehicleModel, string[] i_SpecificDetails)
        {
            throw new NotImplementedException();
        }

        private Vehicle buildFuelMotorcicle(string i_VehicleLicenseNumber, string i_VehicleModel, string[] i_SpecificDetails)
        {
            throw new NotImplementedException();
        }

        private Vehicle buildElectricCar(string i_VehicleLicenseNumber, string i_VehicleModel, string[] i_SpecificDetails)
        {
            throw new NotImplementedException();
        }

        private Vehicle buildFuelCar(string i_VehicleLicenseNumber, string i_VehicleModel, string[] i_SpecificDetails)
        {
            eCarColor color;
            int numOfDoors;
            eFuelType fuelType;
            float tankVolume = 45;
            float currFuelAmount;


            color = (eCarColor) int.Parse(i_SpecificDetails[(int)eFuelCarDetails.Color]);
            numOfDoors = int.Parse(i_SpecificDetails[(int)eFuelCarDetails.NumberOfDoors]);
            fuelType = (eFuelType)int.Parse(i_SpecificDetails[(int)eFuelCarDetails.FuelType]);
            //tankVolume = float.Parse(i_SpecificDetails[3]);
            currFuelAmount = float.Parse(i_SpecificDetails[(int)eFuelCarDetails.CurrFuelAmount]);

            return new Car(i_VehicleModel, i_VehicleLicenseNumber, color, numOfDoors, eTypeOfEnergy.Fuel, fuelType, currFuelAmount, tankVolume);

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
            string[] types = new string[5];
            types[0] = "Fuel car";
            types[1] = "Electric car";
            types[2] = "Fuel motorcicle";
            types[3] = "Electric motorcicle";
            types[4] = "Truck";
            return types;
        }


        public int MinVehicleType
        {
            get { return r_MinVehicleType; }
        }

        public int MaxVehicleType
        {
            get { return r_MaxVehicleType; }
        }        
    }
}
