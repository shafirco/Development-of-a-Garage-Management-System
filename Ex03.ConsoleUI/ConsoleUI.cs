using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ex03.GarageLogic;
using System.Text.RegularExpressions;

namespace Ex03.ConsoleUI
{
    public static class ConsoleUI
    {
        private static Garage m_Garage = new Garage();

        public static void RunGarageSystem()
        {
            eMenuOptions usersChoice = 0;

            printWelcomeMSG();
            do
            {
                try
                {
                    Console.Clear();
                    printMenu();
                    parseUsersChoice(out usersChoice);
                    switch (usersChoice)
                    {
                        case eMenuOptions.AddNewVehicle:
                            {
                                addNewVehicleToGarage();
                                break;
                            }
                        case eMenuOptions.ShowLicenseNumbers:
                            {
                                showLicenseNumbers();
                                break;
                            }
                        case eMenuOptions.ChangeVehicleCondition:
                            {
                                changeVehicleCondition();
                                break;
                            }
                        case eMenuOptions.InflateWheelsToMax:
                            {
                                inflateWheelsToMax();
                                break;
                            }
                        case eMenuOptions.FuelVehicle:
                            {
                                fuelVehicle();
                                break;
                            }
                        case eMenuOptions.ChargeVehicle:
                            {
                                chargeVehicle();
                                break;
                            }
                        case eMenuOptions.ShowInfoByLicense:
                            {
                                showInfoByLicense();
                                break;
                            }
                        case eMenuOptions.Exit:
                            {
                                Console.WriteLine("=====================================================");
                                Console.WriteLine("Thanks for using the system, good bye!");
                                break;
                            }
                    }
                }
                catch (ValueOutOfRangeException valueOutOfRangeException)
                {
                    Console.WriteLine(valueOutOfRangeException.Message);
                }
                catch (FormatException formatException)
                {
                    Console.WriteLine(formatException.Message);
                }
                catch (ArgumentException argumentException)
                {
                    Console.WriteLine(argumentException.Message);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
                finally
                {
                    if (usersChoice != eMenuOptions.Exit)
                    {
                        Console.WriteLine("=====================================================");
                        Console.WriteLine("Press any key to continue..");
                        Console.ReadLine();
                    }
                }
            } while (usersChoice != eMenuOptions.Exit);
        }

        private static void showInfoByLicense()
        {
            if (m_Garage.NumOfVehicles == 0)
            {
                throw new ArgumentException("There is no vehicles in the garage yet.");
            }

            Console.WriteLine("Enter the license number of the vehicle that you want to print:");
            string licenseNumberStr = printVehiclesAndGetInput();
            m_Garage.ShowInfo(licenseNumberStr);
        }

        private static void chargeVehicle()
        {
            if (m_Garage.NumOfVehicles == 0)
            {
                throw new ArgumentException("There is no vehicles in the garage yet.");
            }

            Console.WriteLine("Enter the license number of the vehicle that you want to charge:");
            string licenseNumberStr = printVehiclesAndGetInput();
            if (!m_Garage.HasElectricEngine(licenseNumberStr))
            {
                throw new ArgumentException($"Vehicle {licenseNumberStr} has gas engine, cannot charge it.");
            }
            else if (m_Garage.MaxEnergy(licenseNumberStr) - m_Garage.CurrentEnergy(licenseNumberStr) == 0)
            {
                throw new ArgumentException($"Vehicle {licenseNumberStr} energy is full.");
            }

            float amountToCharge = getAmount(m_Garage.MaxEnergy(licenseNumberStr) - m_Garage.CurrentEnergy(licenseNumberStr), licenseNumberStr);
            m_Garage.AddEnergy(licenseNumberStr, amountToCharge/60f);
            Console.WriteLine("=====================================================");
            Console.WriteLine($"Vehicle {licenseNumberStr} has been charged successfully.");
        }

        private static void fuelVehicle()
        {
            if (m_Garage.NumOfVehicles == 0)
            {
                throw new ArgumentException("There is no vehicles in the garage yet.");
            }

            Console.WriteLine("Enter the license number of the vehicle that you want to fuel:");
            string licenseNumberStr = printVehiclesAndGetInput();
            if (!m_Garage.HasGasEngine(licenseNumberStr))
            {
                throw new ArgumentException($"Vehicle {licenseNumberStr} has electric engine, cannot fuel it.");
            }
            else if (m_Garage.MaxEnergy(licenseNumberStr) - m_Garage.CurrentEnergy(licenseNumberStr) == 0)
            {
                throw new ArgumentException($"Vehicle {licenseNumberStr} energy is full.");
            }

            GasEngine.eFuelType fuelType = getFuelType();
            m_Garage.CheckValidFuel(licenseNumberStr, fuelType);
            float amountToFuel = getAmount(m_Garage.MaxEnergy(licenseNumberStr) - m_Garage.CurrentEnergy(licenseNumberStr), licenseNumberStr);
            m_Garage.AddEnergy(licenseNumberStr, amountToFuel);
            Console.WriteLine("=====================================================");
            Console.WriteLine($"Vehicle {licenseNumberStr} has been fueled successfully.");
        }

        private static float getAmount(float i_PossibleAmountToFill, string i_LicenseNumberStr)
        {
            int sixtyMinutesOrOneLiter;

            if (m_Garage.HasGasEngine(i_LicenseNumberStr))
            {
                sixtyMinutesOrOneLiter = 1;
                Console.WriteLine($"Enter the amount of fuel that you want to add (0-{i_PossibleAmountToFill}):");
            }
            else
            {
                sixtyMinutesOrOneLiter = 60;
                Console.WriteLine($"Enter the amount of energy in minutes that you want to add (0-{i_PossibleAmountToFill * sixtyMinutesOrOneLiter}):");
            }
            
            string amountStr = Console.ReadLine();
            if (!float.TryParse(amountStr, out float amount))
            {
                throw new FormatException("Invalid input.");
            }
            else if (amount < 0 || amount > i_PossibleAmountToFill * sixtyMinutesOrOneLiter)
            {
                throw new ValueOutOfRangeException(i_PossibleAmountToFill * sixtyMinutesOrOneLiter, 0);
            }

            return amount;
        }

        private static GasEngine.eFuelType getFuelType()
        {
            Console.WriteLine("Enter the fuel type:");
            Console.WriteLine("1. Octan95");
            Console.WriteLine("2. Octan96");
            Console.WriteLine("3. Octan98");
            Console.WriteLine("4. Soler");
            string fuelTypeStr = Console.ReadLine();
            if (!int.TryParse(fuelTypeStr, out int fuelTypeNumber))
            {
                throw new FormatException("Invalid choice, enter numbers only.");
            }
            else if (!Enum.IsDefined(typeof(GasEngine.eFuelType), fuelTypeNumber))
            {
                throw new ValueOutOfRangeException(4, 1);
            }

            return (GasEngine.eFuelType)fuelTypeNumber;
        }

        private static void inflateWheelsToMax()
        {
            if (m_Garage.NumOfVehicles == 0)
            {
                throw new ArgumentException("There is no vehicles in the garage yet.");
            }
            Console.WriteLine("Enter the license number of the vehicle that you want to inflate it's wheels to maximum:");
            string licenseNumberStr = printVehiclesAndGetInput();
            m_Garage.InflateToMax(licenseNumberStr);
            Console.WriteLine("=====================================================");
            Console.WriteLine($"Vehicle {licenseNumberStr} wheels inflates to maximum, current tyre pressure: {m_Garage.GetTyrePressure(licenseNumberStr)}");
        }

        private static string printVehiclesAndGetInput()
        {
            m_Garage.PrintVehiclesByCondition(Garage.eVehicleCondition.All);
            string licenseNumberStr = Console.ReadLine();
            if (!int.TryParse(licenseNumberStr, out int licenseNumber))
            {
                throw new FormatException("Invalid input, please tner numbers only.");
            }
            else if (!m_Garage.isExist(licenseNumberStr))
            {
                throw new ArgumentException($"Vehicle {licenseNumberStr} is not exist in the system.");
            }

            return licenseNumberStr;
        }

        private static void changeVehicleCondition()
        {
            if (m_Garage.NumOfVehicles == 0)
            {
                throw new ArgumentException("There is no vehicles in the garage yet.");
            }
            Console.WriteLine("Enter the license number of the vehicle that you want to chage it's condition:");
            string licenseNumberStr = printVehiclesAndGetInput();
            Console.WriteLine($"Current condition of vehicle {licenseNumberStr} is: {m_Garage.GetCondition(licenseNumberStr)}");
            Console.WriteLine("Please enter the new condition:");
            Console.WriteLine("1. Repairing");
            Console.WriteLine("2. Repaired");
            Console.WriteLine("3. Paid");
            string newConditionStr = Console.ReadLine();
            if (!int.TryParse(newConditionStr, out int newConditionChoice))
            {
                throw new FormatException("Invalid input, please enter numbers only.");
            }
            else if (newConditionChoice < 1 || newConditionChoice > 3)
            {
                throw new ValueOutOfRangeException(3, 1);
            }

            Garage.eVehicleCondition newCondition = (Garage.eVehicleCondition)newConditionChoice;
            m_Garage.SetNewCondition(newCondition, licenseNumberStr);
            Console.WriteLine("=====================================================");
            Console.WriteLine($"Vehicle {licenseNumberStr} new condition has changed to: {newCondition} successfully.");
        }

        private static void showLicenseNumbers()
        {
            Console.WriteLine("Which vehicles license numbers do you want to print?:");
            Console.WriteLine("1. Repairing");
            Console.WriteLine("2. Repaired");
            Console.WriteLine("3. Paid");
            Console.WriteLine("4. All vehicles");
            string userInput = Console.ReadLine();
            if (!int.TryParse(userInput, out int vehicleConditionNumber))
            {
                throw new FormatException("Invalid input, please enter numbers only.");
            }
            else if (vehicleConditionNumber < 1 || vehicleConditionNumber > 4)
            {
                throw new ValueOutOfRangeException(4, 1);
            }

            Garage.eVehicleCondition vehicleCondition = (Garage.eVehicleCondition)vehicleConditionNumber;
            m_Garage.PrintVehiclesByCondition(vehicleCondition);
        }

        private static void addNewVehicleToGarage()
        {
            string licenseNumber, ownerName, ownerPhoneNumber;
            VehiclesFactory.eVehicleType vehicleType;
            Vehicle newVehicle;

            getLicenseNumber(out licenseNumber);
            vehicleType = getVehicleType();
            newVehicle = VehiclesFactory.ProduceNewVehicle(vehicleType);
            getBasicVehicleProperties(newVehicle, licenseNumber);
            getUniqueVehicleProperties(newVehicle);
            getOwnerDetails(out ownerName, out ownerPhoneNumber);
            m_Garage.AddNewVehicle(newVehicle, ownerName, ownerPhoneNumber);
            Console.WriteLine("=====================================================");
            Console.WriteLine(string.Format("Vehicle {0} has been added to the garage successfully!", licenseNumber));
        }

        private static void getUniqueVehicleProperties(Vehicle o_NewVehicle)
        {
            List<string> uniqueField = o_NewVehicle.GetFieldsNames();
            List<string> uniqueFieldsValues = new List<string>();

            foreach (string field in uniqueField)
            {
                Console.WriteLine(string.Format("Please enter the {0}", field));
                uniqueFieldsValues.Add(Console.ReadLine());
            }

            VehiclesFactory.SetUniqueVehicleProperties(o_NewVehicle, uniqueFieldsValues);
        }

        private static void getBasicVehicleProperties(Vehicle o_NewVehicle, string i_LicenseNumber)
        {
            string modelStr;

            Console.WriteLine("Please enter the model (produce year) of the vehicle:");
            modelStr = Console.ReadLine();
            Console.WriteLine("Please enter the manufacturer's wheel's name:");
            string manufacturerName = Console.ReadLine();
            Console.WriteLine(string.Format("Please enter the current air pressure (0-{0}):", o_NewVehicle.GetMaxAirPressure().ToString()));
            string currentAirPressureInput = Console.ReadLine();
            if (!float.TryParse(currentAirPressureInput, out float currentAirPressure))
            {
                throw new FormatException("Invalid current air pressure. Please enter a valid number.");
            }

            VehiclesFactory.SetBasicVehicleProperties(o_NewVehicle, i_LicenseNumber, modelStr,
                manufacturerName, currentAirPressure);
        }

        private static void getOwnerDetails(out string o_OwnerName, out string o_OwnerPhoneNumber)
        {
            Console.WriteLine("Please enter the owner's name:");
            o_OwnerName = Console.ReadLine();

            if (string.IsNullOrEmpty(o_OwnerName))
            {
                throw new ArgumentException("Owner's name cannot be null or empty.");
            }

            if (!IsLettersOnly(o_OwnerName))
            {
                throw new FormatException("Owner's name should contain letters only.");
            }

            Console.WriteLine("Please enter the owner's phone number:");
            o_OwnerPhoneNumber = Console.ReadLine();

            if (string.IsNullOrEmpty(o_OwnerPhoneNumber))
            {
                throw new ArgumentException("Owner's phone number cannot be null or empty.");
            }

            if (!IsValidPhoneNumberFormat(o_OwnerPhoneNumber))
            {
                throw new FormatException("Owner's phone number should be in the format 05x-xxxxxxx or 05xxxxxxxx.");
            }
        }

        private static bool IsLettersOnly(string i_StringToCheck)
        {
            return !string.IsNullOrEmpty(i_StringToCheck) && i_StringToCheck.All(char.IsLetter);
        }

        private static bool IsValidPhoneNumberFormat(string i_PhoneNumber)
        {
            return Regex.IsMatch(i_PhoneNumber, @"^05\d(-?\d{7})$");
        }

        private static VehiclesFactory.eVehicleType getVehicleType()
        {
            Console.WriteLine("Enter the vehicle type:");
            Console.WriteLine("1. Gas Motorcycle");
            Console.WriteLine("2. Electric Motorcycle");
            Console.WriteLine("3. Gas Car");
            Console.WriteLine("4. Electric Car");
            Console.WriteLine("5. Truck");

            string userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
            {
                throw new ArgumentException("Invalid vehicle type. Please enter a valid vehicle type.");
            }

            if (!int.TryParse(userInput, out int vehicleTypeNumber) &&
                !Enum.IsDefined(typeof(VehiclesFactory.eVehicleType), vehicleTypeNumber))
            {
                throw new ArgumentException("Invalid vehicle type. Please enter a valid vehicle type.");
            }

            return (VehiclesFactory.eVehicleType)vehicleTypeNumber;
        }

        private static void getLicenseNumber(out string io_LicenseNumber)
        {
            Console.WriteLine("Please enter vehicle's license number:");
            io_LicenseNumber = Console.ReadLine();
            if(!int.TryParse(io_LicenseNumber, out int licenseNumber))
            {
                throw new FormatException("Invalid choice, please enter a valid number.");
            }
            else if (m_Garage.isExist(io_LicenseNumber))
            {
                m_Garage.ChangeVehicleCondition(io_LicenseNumber, Garage.eVehicleCondition.Repairing);
                throw new ArgumentException(string.Format(@"License number {0} is allready exist in the system, changing the vehicle's condition to {1}", io_LicenseNumber, Garage.eVehicleCondition.Repairing));
            }
        }

        private static void checkForValueRangeException(float i_MaxValue, float i_MinValue, float i_Input)
        {
            if (i_Input < i_MinValue || i_Input > i_MaxValue)
            {
                throw new ValueOutOfRangeException(i_MaxValue, i_MinValue);
            }
        }

        private static void parseUsersChoice(out eMenuOptions o_UsersChoice)
        {
            string userInput = Console.ReadLine().Trim();

            if (!int.TryParse(userInput, out int userNumber))
            {
                throw new FormatException("Invalid choice, please enter a valid number.");
            }

            checkForValueRangeException((int)eMenuOptions.Exit, (int)eMenuOptions.AddNewVehicle, userNumber);
            o_UsersChoice = (eMenuOptions)userNumber;
        }

        private static void printMenu()
        {
            StringBuilder menu = new StringBuilder();

            menu.AppendLine("Menu:");
            menu.AppendLine("=====================================================");
            menu.AppendLine("1. Enter a new Vehicle to the garage");
            menu.AppendLine("2. Show license numbers of the vehicles in the garage");
            menu.AppendLine("3. Change vehicle condition");
            menu.AppendLine("4. Inflate tyres to maximum");
            menu.AppendLine("5. Fuel a vehicle (Gas vehicle)");
            menu.AppendLine("6. Charge a vehicle (Electric vehicle)");
            menu.AppendLine("7. Show full details of a vehicle");
            menu.AppendLine("8. Exit");
            menu.AppendLine("=====================================================");
            Console.Write(menu);
        }

        private static void printWelcomeMSG()
        {
            StringBuilder welcomeMSG = new StringBuilder();

            welcomeMSG.AppendLine("==============================================");
            welcomeMSG.AppendLine("Hello and welcome to garage management system!");
            welcomeMSG.AppendLine("==============================================");
            Console.Write(welcomeMSG);
            Thread.Sleep(2500);
        }

        enum eMenuOptions
        {
            AddNewVehicle = 1,
            ShowLicenseNumbers,
            ChangeVehicleCondition,
            InflateWheelsToMax,
            FuelVehicle,
            ChargeVehicle,
            ShowInfoByLicense,
            Exit
        }
    }
}
