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
        private const int k_MinLicenseLength = 1;
        private const int k_MinVehicleType = 1;
        private const int k_MaxVehicleType = 5;

        public ConsoleUI()
        {
            m_garage = new GarageLogic.Garage();
            m_VehicleBuilder = new VehicleBuilder();

            //DEBUG START

            Vehicle v1 = m_VehicleBuilder.buildVehicle(eVehicleType.FuelCar);
            v1.LicenseNum = "123";

            m_garage.AddVehicle(v1);
            //DEBUG END

            Run();
        }

        public void Run()
        {
            
            eMenuOption menuOption = eMenuOption.Exit;
            do
            {
                printMainMenuOptions();
                menuOption = getMenuOptionFromUser();

                Console.Clear();
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

            try
            {
                m_garage.Inflate(licenceNumber);
                write("Inflating successful!");
            }
            catch (NoSuchVehicleException e)
            {
                write("No such vehicle");
            }
        }

        private void showVehicleLicenses()
        {
            bool isGoodInput = false;
            int option;

            do
            {
                write("Please choose your filter:");
                printVehicleStatusMenu();

                string optionAsString = Console.ReadLine();
               
                if (!int.TryParse(optionAsString, out option) || option < 0 || option > 3)
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

                printVehicles(m_garage.GetFuelVehicles);
                printVehicles(m_garage.GetElectricVehicles);
            }
            else
            {
                write(String.Format("Listing vehicles in status {0}:", Enum.GetName(typeof(eVehicleStatus), option)));
                printVehicles(m_garage.GetFuelVehicles, (eVehicleStatus)option);
                printVehicles(m_garage.GetElectricVehicles, (eVehicleStatus)option);
            }
        }

        private void printVehicleStatusMenu()
        {
            Console.WriteLine("{0}. {1}", 0, "All statuses");
            Array types = Enum.GetValues(typeof(eVehicleStatus));
          
            foreach (eVehicleStatus type in types)
            {
                Console.WriteLine("{0}. {1}", (int)type, Enum.GetName(typeof(eVehicleStatus), type));
                
            }
           
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
                    Console.WriteLine("Licence {0} is {1}", entry.Key, entry.Value.VehicleStatus);
                }
            }
        }

       /// <summary>
       /// 
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
            string licenseNumber = getLicenceNumber();

            Vehicle vehicle = m_garage.GetVehicle(licenseNumber);

            if (vehicle != null)
            {
                write(vehicle.Print());
            }
            else
            {
                write("No such vehicle");
            }
        }

        private void changeVehicleStatus()
        {
            string licenseNumber = getLicenceNumber();

            Vehicle vehicle = m_garage.GetVehicle(licenseNumber);
            
            if (vehicle != null)
            {
                eVehicleStatus status = getVehicleStatus();
                m_garage.ChangeStatus(vehicle, status);
            }
            else
            {
                write(String.Format("No such vehicle in the garage: {0}", licenseNumber));
            }
        }

        private eVehicleStatus getVehicleStatus()
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

        private void enterVehicle()
        {

            string licenseNumber = getLicenceNumber();
            Vehicle vehicle = m_garage.GetVehicle(licenseNumber);

            // No such vehicle, add it
            if (vehicle == null)
            {
                eVehicleType typeOfVehicle = getVehicleType();
                vehicle = m_VehicleBuilder.buildVehicle(typeOfVehicle);
                vehicle.LicenseNum = licenseNumber;
                m_garage.AddVehicle(vehicle);
                
                write("Vehicle was successfuly added!");
            }
            // Already exist, change status
            else
            {
                m_garage.ChangeStatus(vehicle, eVehicleStatus.InReparation);
                write(String.Format("Vehicle status changed to {0}", eVehicleStatus.InReparation));
            }
        }

        private string getLicenceNumber()
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

        private eVehicleType getVehicleType()
        {
            bool isGoodInput = false;
            int vehicleType = 0;

            do
            {
                printVehicleTypeMenu();
                //System.Console.WriteLine("Please enter desired option ({0}-{1}):", 1, 5);
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

            return (eVehicleType)vehicleType;
        }

        private void printVehicleTypeMenu()
        {
            string[] types = Enum.GetNames(typeof(eVehicleType));
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
