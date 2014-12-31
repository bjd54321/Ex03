﻿using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.GarageManagementSystem.ConsoleUI
{
    public class ConsoleUI
    {
        private GarageLogic.Garage m_Garage;

        private GarageLogic.VehicleBuilder m_VehicleBuilder;

        private const int k_MinMainMenuOption = 1;
        private const int r_MaxMainMenuOption = 8;
        
        private const int k_MinLicenseLength = 1;
        private const int k_MaxLicenseLength = 10;

        private const int k_MinVehicleType = 1;
        private const int k_MaxVehicleType = 5;

        // Indicates vehicles in all statuses
        private const int k_AllStatuses = 0;

        private const int k_MinVehicleStatus = 0;
        private const int k_MaxVehicleStatus = 3;

        public ConsoleUI()
        {
            m_Garage = new GarageLogic.Garage();
            m_VehicleBuilder = new VehicleBuilder();

            //DEBUG START

            Vehicle v1 = m_VehicleBuilder.buildVehicle(VehicleBuilder.eVehicleType.FuelCar);
            v1.LicenseNum = "123";

            m_Garage.AddVehicle(v1);
            //DEBUG END

            Run();
        }

        public void Run()
        {
            
            eMenuOption menuOption = eMenuOption.Exit;
            do
            {
                printMainMenuOptions();
                menuOption = getMainMenuOptionFromUser();

                Console.Clear();
                performSelectedOption(menuOption);
            } while(menuOption != eMenuOption.Exit);
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
            string licenseNumber = getLicenceNumberFromUser();
            float chargeAmount = getChargeAmountFromUser();

            Vehicle electricVehicle = null;
            if (m_Garage.GetElectricVehicles.TryGetValue(licenseNumber, out electricVehicle))
            {
                m_Garage.Charge(electricVehicle, chargeAmount);
            }
            else
            {
                write("No fuel vehicle listed for license number " + licenseNumber);
            }
        }

        private float getChargeAmountFromUser()
        {
            bool isGoodInput = false;
            float chargeAmount = 0;

            do
            {
                write("Please enter the amount of charge:");

                string optionAsString = Console.ReadLine();

                if (!float.TryParse(optionAsString, out chargeAmount) || chargeAmount <= 0)
                {
                    System.Console.WriteLine("The input you entered is invalid. Charge amount must be positive.");
                }
                else
                {
                    isGoodInput = true;
                }

            } while (!isGoodInput);

            return chargeAmount;
        }


        /// <summary>
        /// Allows user to select a vehicle and fuel it
        /// </summary>
        private void putFuel()
        {
            string licenseNumber = getLicenceNumberFromUser();
            eFuelType fuelType = getFuelTypeFromUser();
            float fuelAmount = getFuelAmountFromUser();

            Vehicle fuelVehicle = null;
            if (m_Garage.GetFuelVehicles.TryGetValue(licenseNumber, out fuelVehicle))
            {
                try
                {
                    m_Garage.AddFuel(fuelVehicle, fuelAmount, fuelType);
                    write("Vehicle was successfully fueled!");
                }
                catch (ArgumentException argumentException)
                {
                    write(argumentException.ToString());
                }                
            }
            else
            {
                write("No fuel vehicle listed for license number " + licenseNumber);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private float getFuelAmountFromUser()
        {
            bool isGoodInput = false;
            float fuelAmount = 0;

            do
            {
                write("Please enter the fuel amount:");
           
                string optionAsString = Console.ReadLine();

                if (!float.TryParse(optionAsString, out fuelAmount) || fuelAmount <= 0)
                {
                    System.Console.WriteLine("The input you entered is invalid. Fuel amount must be positive.");
                }
                else
                {
                    isGoodInput = true;
                }

            } while (!isGoodInput);

            return fuelAmount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private eFuelType getFuelTypeFromUser()
        {
         
            bool isGoodInput = false;
            int option;

            do
            {
                write("Please choose fuel type:");
                printFuelTypeMenu();
            
                string optionAsString = Console.ReadLine();

                if (!int.TryParse(optionAsString, out option) || option < 1 || option > 4)
                {
                    System.Console.WriteLine("The input you entered is invalid.");
                }
                else
                {
                    isGoodInput = true;
                }

            } while (!isGoodInput);

            return (eFuelType)option;
        }

        /// <summary>
        /// 
        /// </summary>
        private void printFuelTypeMenu()
        {
            Array types = Enum.GetValues(typeof(eFuelType));

            foreach (eFuelType type in types)
            {
                Console.WriteLine("{0}. {1}", (int)type, Enum.GetName(typeof(eFuelType), type));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void inflateTires()
        {   
            string licenceNumber = getLicenceNumberFromUser();

            try
            {
                m_Garage.Inflate(licenceNumber);
                write("Inflating successful!");
            }
            catch (NoSuchVehicleException e)
            {
                write("No such vehicle");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void showVehicleLicenses()
        {
            bool isGoodInput = false;
            int option;

            do
            {
                write("Please choose your filter:");
                printVehicleStatusMenu(true);

                string optionAsString = Console.ReadLine();
               
                if (!int.TryParse(optionAsString, out option) || option < k_MinVehicleStatus || option > k_MaxVehicleStatus)
                {
                    System.Console.WriteLine("The input you entered is invalid.");
                }
                else
                {
                    isGoodInput = true;
                }
                
            } while (!isGoodInput);

            if (option == 0)
            {
                write("Listing all vehicles in the garage:");

                printVehicles(m_Garage.GetFuelVehicles);
                printVehicles(m_Garage.GetElectricVehicles);
            }
            else
            {
                write(String.Format("Listing vehicles in status {0}:", Enum.GetName(typeof(eVehicleStatus), option)));
                printVehicles(m_Garage.GetFuelVehicles, (eVehicleStatus)option);
                printVehicles(m_Garage.GetElectricVehicles, (eVehicleStatus)option);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i_ShowAllStatuses"></param>
        private void printVehicleStatusMenu(bool i_ShowAllStatuses)
        {
            if (i_ShowAllStatuses)
            {
                Console.WriteLine("{0}. {1}", k_AllStatuses, "All statuses");
            }
            
            Array types = Enum.GetValues(typeof(eVehicleStatus));
          
            foreach (eVehicleStatus type in types)
            {
                Console.WriteLine("{0}. {1}", (int)type, Enum.GetName(typeof(eVehicleStatus), type));
                
            }           
        }

        /// <summary>
        /// Overload for printing vehicles in all statuses
        /// </summary>
        /// <param name="vehicles"></param>
        private void printVehicles(Dictionary<string, Vehicle> vehicles)
        {
            printVehicles(vehicles, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicles"></param>
        /// <param name="status"></param>
        private void printVehicles(Dictionary<string, Vehicle> vehicles, eVehicleStatus? status)
        {
            foreach (KeyValuePair<string, Vehicle> entry in vehicles)
            {
                if (!status.HasValue || status.Equals(entry.Value.VehicleStatus))
                {
                    Console.WriteLine("Licence {0} is {1}", entry.Key, entry.Value.VehicleStatus);
                }
            }
        }

       /// <summary>
       /// Helper for printing a simple message to user
       /// </summary>
       /// <param name="message"></param>
        private void write(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// 
        /// </summary>
        private void showDetailsByLicenceNumber()
        {
            string licenseNumber = getLicenceNumberFromUser();

            Vehicle vehicle = m_Garage.GetVehicle(licenseNumber);

            if (vehicle != null)
            {
                write(vehicle.Print());
            }
            else
            {
                write("No such vehicle");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void changeVehicleStatus()
        {
            string licenseNumber = getLicenceNumberFromUser();

            Vehicle vehicle = m_Garage.GetVehicle(licenseNumber);
            
            if (vehicle != null)
            {
                printVehicleStatusMenu(false);
                eVehicleStatus status = getVehicleStatusFromUser();
                m_Garage.ChangeStatus(vehicle, status);
            }
            else
            {
                write(String.Format("No such vehicle in the garage: {0}", licenseNumber));
            }
        }

        private eVehicleStatus getVehicleStatusFromUser()
        {
            bool isGoodInput = false;
            int status = 0;

            do
            {
                System.Console.WriteLine("Please enter desired option ({0}-{1}):", 1, 3);
                string inputText = System.Console.ReadLine();
              
                if (!int.TryParse(inputText, out status) || status < 1 || status > 3)
                {
                    System.Console.WriteLine("The input you entered is invalid.");
                }
                else
                {
                    isGoodInput = true;
                }
            } while (!isGoodInput);

            return (eVehicleStatus)status;
        }

        /// <summary>
        /// 
        /// </summary>
        private void enterVehicle()
        {

            string licenseNumber = getLicenceNumberFromUser();
            Vehicle vehicle = m_Garage.GetVehicle(licenseNumber);

            // No such vehicle, add it
            if (vehicle == null)
            {
                VehicleBuilder.eVehicleType typeOfVehicle = getVehicleTypeFromUser();
                vehicle = m_VehicleBuilder.buildVehicle(typeOfVehicle);
                vehicle.LicenseNum = licenseNumber;
                vehicle.OwnerName = getOwnerNameFromUser();
                vehicle.OwnerPhone = getOwnerPhoneFromUser();
                m_Garage.AddVehicle(vehicle);
                
                write("Vehicle was successfuly added!");
            }
            // Already exist, change status
            else
            {
                m_Garage.ChangeStatus(vehicle, eVehicleStatus.InReparation);
                write(String.Format("Vehicle status changed to {0}", eVehicleStatus.InReparation));
            }
        }

        /// <summary>
        /// Get name in a naive way
        /// We only require that the name contains at least two letters
        /// </summary>
        /// <returns></returns>
        private string getOwnerNameFromUser()
        {
            bool isValidInput = false;
            string ownerName = "";

            do
            {
                write("Please enter your name: ");
                ownerName = Console.ReadLine();
                if (ownerName.Length > 1)
                {
                    isValidInput = true;
                }
            } while (!isValidInput);

            return ownerName;
        }

        private string getOwnerPhoneFromUser()
        {
            bool isValidInput = false;
            string ownerPhone = "";

            do
            {
                write("Please enter your phone number. Only digits and '-' are allowed");
                ownerPhone = Console.ReadLine();
                if (isPhoneValid(ownerPhone))
                {
                    isValidInput = true;
                }
            } while (!isValidInput);

            return ownerPhone;
        }

        /// <summary>
        /// Validates phone without using regex
        /// No complex validation, eg 1, 123-123 are all valid phones
        /// </summary>
        /// <param name="i_OwnerPhone"></param>
        /// <returns></returns>
        private bool isPhoneValid(string i_OwnerPhone)
        {
            bool isValid = true;
            char[] ownerPhoneAsCharArray = i_OwnerPhone.ToCharArray();
            if (i_OwnerPhone.Trim().Replace("-", "").Length == 0)
            {
                isValid = false;
            }
            else
            {
                for (int i = 0; i < i_OwnerPhone.Length; i++)
                {
                    if (!Char.IsDigit(ownerPhoneAsCharArray[i])
                        && ownerPhoneAsCharArray[i] != '=')
                    {
                        isValid = false;
                        break;
                    }
                }
            }


            return isValid;
        }

        private string getLicenceNumberFromUser()
        {
            bool isGoodInput = false;
            string licenseNumber = "";

            write("Please enter vehicle license number:");
            do
            {
                licenseNumber = Console.ReadLine();
                if (licenseNumber.Length >= k_MinLicenseLength)
                {
                    isGoodInput = true;
                }
                else
                {
                    write(String.Format("License number must be at least {0} characters long", k_MinLicenseLength)); 
                }
            } while (!isGoodInput);

            return licenseNumber;
        }

        /// <summary>
        /// Gets valid vehicle type from user
        /// </summary>
        /// <returns>Enum that represents vehicle type</returns>
        private VehicleBuilder.eVehicleType getVehicleTypeFromUser()
        {
            bool isGoodInput = false;
            int vehicleType = 0;

            do
            {                
                printVehicleTypeMenu();
                string inputText = System.Console.ReadLine();

                if (!int.TryParse(inputText, out vehicleType) || vehicleType < k_MinVehicleType || vehicleType > k_MaxVehicleType)
                {
                    System.Console.WriteLine("The input you entered is invalid.");
                }
                else
                {
                    isGoodInput = true;
                }
            } while (!isGoodInput);

            return (VehicleBuilder.eVehicleType)vehicleType;
        }

        /// <summary>
        /// Displays vehicle types menu to user
        /// </summary>
        private void printVehicleTypeMenu()
        {
            write("Please choose vehicle type");
            string[] types = m_Garage.getVehicleTypesAsStrings();
            int count = 1;
            foreach (string type in types)
            {
                Console.WriteLine("{0}. {1}", count, type);
                count++;
            }
        }

        private void printMainMenuOptions()
        {
            Console.WriteLine("Please choose action:" + Environment.NewLine);
            for (int i = 1; i <= r_MaxMainMenuOption; i++)
            {
                printMenuOption((eMenuOption)i);
            }            
        }

        private eMenuOption getMainMenuOptionFromUser()
        {
            bool numberIsInt = false;
            bool goodInput = false;
            int menuOption = 0;

            do
            {
                System.Console.WriteLine("Please enter desired option ({0}-{1}):", k_MinMainMenuOption, r_MaxMainMenuOption);
                string inputText = System.Console.ReadLine();
                numberIsInt = int.TryParse(inputText, out menuOption);
                if (!numberIsInt || menuOption < k_MinMainMenuOption || menuOption > r_MaxMainMenuOption)
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
