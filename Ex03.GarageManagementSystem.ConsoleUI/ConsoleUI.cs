using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.GarageManagementSystem.ConsoleUI
{
    public class ConsoleUI
    {
        private GarageLogic.Garage m_garage;
        private GarageLogic.VehicleBuilder m_VehicleBuilder;
        private readonly int r_NumOfMenuOptions = 8;
        private readonly int r_MinOption = 1;

        public ConsoleUI()
        {
            m_garage = new GarageLogic.Garage();
            Run();
        }

        public void Run()
        {
            eMenuOption menuOption = eMenuOption.Exit;
            do
            {
                printMenuOptions();
                menuOption = getMenuOptionFromUser();
                performSelectedOption(menuOption);
            }while(menuOption != eMenuOption.Exit);
        }

        private void performSelectedOption(eMenuOption menuOption)
        {
            switch (menuOption)
            {
                case eMenuOption.EnterVehicleToGarage:
                    enterVehicle();
                    break;
                case eMenuOption.ShowVehicleLicenses:
                    showVehicleLicenses();
                    break;
                case eMenuOption.ChangeVehicleStatus:
                    changeVehicleStatus();
                    break;
                case eMenuOption.InflateTires:
                    inflateTires();
                    break;
                case eMenuOption.PutFuel:
                    putFuel();
                    break;
                case eMenuOption.Charge:
                    charge();
                    break;
                case eMenuOption.ShowDetailsByLicenceNumber:
                    showDetailsByLicenceNumber();
                    break;
                case eMenuOption.Exit:
                    break;
                default:
                    break;
            }
        }

        private void charge()
        {
            string licenseNumber = getLicenceNumber();
            float chargeAmount = getChargeAmount();

            Vehicle electricVehicle = null;
            if (m_garage.GetElectricVehicles.TryGetValue(licenseNumber, out electricVehicle))
            {
                m_garage.Charge(electricVehicle, chargeAmount);
            }
            else
            {
                write("No fuel vehicle listed for license number " + licenseNumber);
            }
        }

        private float getChargeAmount()
        {
            throw new NotImplementedException();
        }

        private void putFuel()
        {
            string licenseNumber = getLicenceNumber();
            eFuelType fuelType = getFuelType();
            float fuelAmount = getFuelAmount();

            Vehicle fuelVehicle = null;
            if (m_garage.GetFuelVehicles.TryGetValue(licenseNumber, out fuelVehicle))
            {
                m_garage.AddFuel(fuelVehicle, fuelAmount, fuelType);
            }
            else
            {
                write("No fuel vehicle listed for license number " + licenseNumber);
            }
        }

        private float getFuelAmount()
        {
            write("Please enter the fuel amount");
            return 0f;
        }

        private eFuelType getFuelType()
        {
            write("Please enter fuel type");
            return eFuelType.Octan95;
        }

        private void inflateTires()
        {   
            string licenceNumber = getLicenceNumber();

            m_garage.Inflate(licenceNumber);
        }

        private void showVehicleLicenses()
        {
            write("Please choose status of vehicles");

            printVehicles(m_garage.GetFuelVehicles);
            printVehicles(m_garage.GetElectricVehicles);
            
        }

        private void printVehicles(Dictionary<string, Vehicle> vehicles)
        {
            printVehicles(vehicles, null);
        }

        private void printVehicles(Dictionary<string, Vehicle> vehicles, eVehicleStatus? status)
        {
            foreach (KeyValuePair<string, Vehicle> entry in vehicles)
            {
                if (!status.HasValue || status.Equals(entry.Value.VehicleStatus))
                {
                    Console.WriteLine("Licence %s status %s", entry.Key, entry.Value.LicenseNum);
                }
            }
        }

        /*
         * Shortcut for writing to output
         */
        private void write(string message)
        {
            Console.WriteLine(message);
        }

        private void showDetailsByLicenceNumber()
        {
            string licenseNumber = getLicenceNumber();

            Vehicle vehicle = m_garage.GetVehicle(licenseNumber);

            StringBuilder details = new StringBuilder();
            details.Append(String.Format(
@"License number: {0}
Brand name: {1}
Owner's name: {2}
Vehicle's status: {3}", vehicle.LicenseNum, "", "", vehicle.VehicleStatus));
            details.Append(vehicle.GetTireDetails());
            details.Append(vehicle.getAdditionalDetails());
            Console.WriteLine("");
        }

        private void changeVehicleStatus()
        {
            string licenseNumber = getLicenceNumber();
            eVehicleStatus status = getVehicleStatus();

            Vehicle vehicle = m_garage.GetVehicle(licenseNumber);
            
            if (vehicle != null)
            {
                m_garage.ChangeStatus();
            }
        }

        private eVehicleStatus getVehicleStatus()
        {
            throw new NotImplementedException();
        }

        private void enterVehicle()
        {
            VehicleBuilder.eTypeOfVehicle typeOfVehicle = getVehicleType();
            Vehicle vehicle = m_VehicleBuilder.buildVehicle(typeOfVehicle);

            vehicle.LicenseNum = getLicenceNumber();
            m_garage.AddVehicle(vehicle);
        }

        private string getLicenceNumber()
        {
            throw new NotImplementedException();
        }

        private VehicleBuilder.eTypeOfVehicle getVehicleType()
        {
            throw new NotImplementedException();
        }

        private void printMenuOptions()
        {
            Console.WriteLine("Please choose action:" + Environment.NewLine);
            for (int i = 1; i <= r_NumOfMenuOptions; i++)
            {
                printMenuOption((eMenuOption)i);
            }            
        }

        private eMenuOption getMenuOptionFromUser()
        {
            bool numberIsInt = false;
            bool goodInput = false;
            int menuOption = 0;

            do
            {
                System.Console.WriteLine("Please enter desired option ({0}-{1}):", r_MinOption, r_NumOfMenuOptions);
                string inputText = System.Console.ReadLine();
                numberIsInt = int.TryParse(inputText, out menuOption);
                if (!numberIsInt || menuOption < r_MinOption || menuOption > r_NumOfMenuOptions)
                {
                    System.Console.WriteLine("The input you entered is invalid.");
                }
                else
                {
                    goodInput = true;
                }
            }
            while (!goodInput);

            return (eMenuOption) menuOption;
        }


        private void printMenuOption(eMenuOption i_MenuOption)
        {
            StringBuilder menuOptionText = new StringBuilder();
            menuOptionText.Append((int)i_MenuOption + ". ");
            
            switch (i_MenuOption)
            {
                case eMenuOption.EnterVehicleToGarage:
                    menuOptionText.Append("Enter vehicle to garage");
                    break;
                case eMenuOption.ShowVehicleLicenses:
                    menuOptionText.Append("Show list of vehicle licenses");
                    break;
                case eMenuOption.ChangeVehicleStatus:
                    menuOptionText.Append("Change vehicle status");
                    break;
                case eMenuOption.InflateTires:
                    menuOptionText.Append("Inflate vehicle tires");
                    break;
                case eMenuOption.PutFuel:
                    menuOptionText.Append("Put fuel on vehicle");
                    break;
                case eMenuOption.Charge:
                    menuOptionText.Append("Charge electric vehicle");
                    break;
                case eMenuOption.ShowDetailsByLicenceNumber:
                    menuOptionText.Append("Show vehicle details");
                    break;
                case eMenuOption.Exit:
                    menuOptionText.Append("Exit");
                    break;
                default:
                    break;
            }

            Console.WriteLine(menuOptionText);
        }

   

    }
}
