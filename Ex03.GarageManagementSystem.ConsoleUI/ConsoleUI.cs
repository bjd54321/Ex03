using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.GarageManagementSystem.ConsoleUI
{
    public class ConsoleUI
    {
        private GarageLogic.Garage m_garage;
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
                case eMenuOption.ShowDetailsByLicenceNum:
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

        private void showLicenseNumbers()
        {
            List<Vehicle> vehiclesList = m_garage.VehiclesList;
            foreach (Vehicle vehicle in vehiclesList)
            {
                Console.WriteLine(vehicle.LicenseNum);
            }
        }

        private void showLicenseNumbers(eVehicleStatus i_VehicleStatusToFilterBy)
        {
            List<Vehicle> vehiclesList = m_garage.VehiclesList;
            foreach (Vehicle vehicle in vehiclesList)
            {
                if(vehicle.VehicleStatus == i_VehicleStatusToFilterBy)
                {
                    Console.WriteLine(vehicle.LicenseNum);
                }
            }
        }

        private void changeVehicleStatus(string i_LicenseNum, eVehicleStatus i_VehicleStatus)
        {
            List<Vehicle> vehiclesList = m_garage.VehiclesList;
            foreach (Vehicle vehicle in vehiclesList)
            {
                if(vehicle.LicenseNum == i_LicenseNum)
                {
                    vehicle.VehicleStatus = i_VehicleStatus;
                    break;
                }
            }
        }

        private void inflateTires(string i_LicenseNum)
        {
            List<Vehicle> vehiclesList = m_garage.VehiclesList;
            foreach (Vehicle vehicle in vehiclesList)
            {
                if (vehicle.LicenseNum == i_LicenseNum)
                {
                    vehicle.inflateTires();
                    break;
                }
            }
        }

        private void showVehicleDetails(string i_LicenseNum)
        {
            List<Vehicle> vehiclesList = m_garage.VehiclesList;
            foreach (Vehicle vehicle in vehiclesList)
            {
                if (vehicle.LicenseNum == i_LicenseNum)
                {
                    StringBuilder details = new StringBuilder();
                    details.Append(
@"License number: {0}
Brand name: {1}
Owner's name: {2}
Vehicle's status: {3}");
                    details.Append(vehicle.getTireDetails());
                    details.Append(vehicle.getAdditionalDetails());
                    Console.WriteLine("");
                    break;
                }
            }            
        }
    }
}
