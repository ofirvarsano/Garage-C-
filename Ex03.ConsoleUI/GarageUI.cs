using System;
using System.Collections.Generic;
using System.Threading;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageUI
    {
        public enum eGarageMenuOptions
        {
            AddVehicleToTheGarage = 1,
            ShowAllVehiclesInTheGarage = 2,
            ChangeVehicleStatus = 3,
            FillAirPressure = 4,
            LoadEnergy = 5,
            ShowVehicleDetails = 6,
            Exit = 7
        }

        private eGarageMenuOptions m_GarageMenuOption;
        private GarageAction m_GarageAction = new GarageAction();

        public eGarageMenuOptions GarageMenuOption
        {
            get
            {
                return m_GarageMenuOption;
            }

            set
            {
                m_GarageMenuOption = value;
            }
        }

        public GarageAction CurrentGarageAction
        {
            get
            {
                return m_GarageAction;
            }
        }

        public void BeginGarageUI()
        {
            startGarage();
        }

        private void startGarage()
        {
            bool isUserWantToExit = false;
            string userChoice = string.Empty;
            int milisecondsToSleep = 1000;
            while (!isUserWantToExit)
            {
                printMainMenuOptions();
                userChoice = Console.ReadLine();
                try
                {
                    isUserWantToExit = mainMenuActions(userChoice);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine("Loading... Please Wait");
                Thread.Sleep(milisecondsToSleep);
            }

            Console.WriteLine("Goodbye see you again!!!");
        }

        private bool mainMenuActions(string i_UserChoice)
        {
            const bool k_IsUserWantTOExit = false;
            GarageMenuOption = (eGarageMenuOptions)Enum.Parse(typeof(eGarageMenuOptions), i_UserChoice);
            switch (GarageMenuOption)
            {
                case eGarageMenuOptions.AddVehicleToTheGarage:
                    addVehicleToTheGarage();
                    break;
                case eGarageMenuOptions.ShowAllVehiclesInTheGarage:
                    showAllVehiclesInTheGarageByLicenseNumber();
                    break;
                case eGarageMenuOptions.ChangeVehicleStatus:
                    changeVehicleStatus();
                    break;
                case eGarageMenuOptions.FillAirPressure:
                    fillAirPressure();
                    break;
                case eGarageMenuOptions.LoadEnergy:
                    loadEnergy();
                    break;
                case eGarageMenuOptions.ShowVehicleDetails:
                    showVehicleDetails();
                    break;
                case eGarageMenuOptions.Exit:
                    return !k_IsUserWantTOExit;
                default:
                    throw new ArgumentException(string.Format("Enter valid number! {0}", GarageMenuOption));
            }

            return k_IsUserWantTOExit;
        }

        private void addVehicleToTheGarage()
        {
            string licenseNumber = string.Empty;
            string message = string.Empty;
            menuChoiceTitle("Add New Vehicle");
            Console.Write("Please enter license number: ");
            licenseNumber = Console.ReadLine();
            string licenseCopy = licenseNumber.Trim();
            if (string.IsNullOrEmpty(licenseNumber) || licenseCopy.Length == 0)
            {
                throw new ArgumentException("License cant be empty");
            }

            if (CurrentGarageAction.CheckIfVehicleExistAndChangeStatus(licenseNumber, out message))
            {
                Console.Write("Please enter owner name: ");
                string ownerName = Console.ReadLine();
                Console.Write("Please enter owner phone: ");
                string ownerPhone = Console.ReadLine();
                int typeOfVehicle = getVehicleType();
                try
                {
                    CurrentGarageAction.AddCarToGarage(ownerName, ownerPhone, licenseNumber, typeOfVehicle);
                    addProperties();
                    addWheels();
                    Console.WriteLine(message);
                }
                catch (ArgumentException ax)
                {
                    Console.WriteLine(ax.Message);
                }
            }
            else
            {
                Console.WriteLine(message);
            }
        }

        private void addWheels()
        {
            Console.Write("Please enter wheels manufacturer name: ");
            string manufacturer = Console.ReadLine();
            float currentAirPressure = 0;
            bool isInputValid = false;
            do
            {
                Console.Write("Please enter current wheels air pressure:");
                if (float.TryParse(Console.ReadLine(), out currentAirPressure) && currentAirPressure > 0)
                {
                    try
                    {
                        CurrentGarageAction.AddWheels(manufacturer, currentAirPressure);
                        isInputValid = true;
                    }
                    catch (ValueOutOfRangeException vorex)
                    {
                        Console.WriteLine(vorex.Message);
                        Console.WriteLine("value are between {0} to {1}", vorex.MinValue, vorex.MaxValue);
                    }
                }
                else
                {
                    Console.WriteLine("You have entered wrong input!");
                }
            }
            while (!isInputValid);
        }

        private void addProperties()
        {
            Dictionary<int, string> properties = CurrentGarageAction.GetVehicleProperties();
            bool isValid = false;

            for (int i = 1; i <= properties.Count; i++)
            {
                isValid = false;
                Console.Write(properties[i]);

                do
                {
                    try
                    {
                        CurrentGarageAction.SetProperty(i, Console.ReadLine());
                        isValid = true;
                    }
                    catch (ValueOutOfRangeException vorex)
                    {
                        Console.WriteLine(vorex.Message);
                        Console.WriteLine("value are between {0} to {1}", vorex.MinValue, vorex.MaxValue);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                while (!isValid);
            }
        }

        private int getVehicleType()
        {
            string userChoiceStr = string.Empty;
            int userChoice = 0;
            bool isInputValid = false;
            do
            {
                Console.WriteLine("Vehicles Types:");
                Console.Write(CurrentGarageAction.GetTypesOfVehicles());
                Console.Write("Press your choice: ");
                userChoiceStr = Console.ReadLine();
                try
                {
                    if (int.TryParse(userChoiceStr, out userChoice))
                    {
                        isInputValid = CurrentGarageAction.IsUserVehicleTypeChoiceLegal(userChoice);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            while (!isInputValid);
            return userChoice;
        }

        private void showAllVehiclesInTheGarageByLicenseNumber()
        {
            menuChoiceTitle("Show All Vehicles In The Garage");
            int choice = 0;
            string choiceStr = string.Empty;
            bool isInputValid = true;
            do
            {
                isInputValid = true;
                Console.WriteLine("Filter by status? 1 - Yes or 2 - No.");
                Console.Write("press: ");
                choiceStr = Console.ReadLine();
                if (int.TryParse(choiceStr, out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            showListOfAllVehiclesByStatus();
                            break;
                        case 2:
                            Console.WriteLine(CurrentGarageAction.GetListOfVehiclesInGarage());
                            break;
                        default:
                            isInputValid = false;
                            break;
                    }
                }
                else
                {
                    isInputValid = false;
                }
            }
            while (!isInputValid);
            askForBackToMainMenu();
        }

        private void showListOfAllVehiclesByStatus()
        {
            int userChoice = 0;
            string userChoiceStr = string.Empty;
            bool isInputValid = false;
            do
            {
                Console.WriteLine(CurrentGarageAction.GetListOfStatus());
                Console.Write("press: ");
                userChoiceStr = Console.ReadLine();
                if (int.TryParse(userChoiceStr, out userChoice))
                {
                    try
                    {
                        Console.WriteLine(CurrentGarageAction.GetListOfVehiclesInGarageByStatus(userChoice));
                        isInputValid = true;
                    }
                    catch (ValueOutOfRangeException vorex)
                    {
                        Console.WriteLine(vorex.Message);
                        Console.WriteLine("value are between {0} to {1}", vorex.MinValue, vorex.MaxValue);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            while (!isInputValid);
        }

        private void changeVehicleStatus()
        {
            menuChoiceTitle("Change Vehicle Status");
            string statusStr = string.Empty;
            bool isVehicleSet = true;
            bool isVehicleStatusWasChanged = false;
            int status = 0;

            try
            {
                isVehicleSet = setLicenseNumber();
                if (isVehicleSet)
                {
                    Console.WriteLine("Choose Status:");
                    Console.WriteLine(CurrentGarageAction.GetListOfStatus());
                    statusStr = Console.ReadLine();
                    if (int.TryParse(statusStr, out status))
                    {
                        CurrentGarageAction.TryChangeVehicleStatus(status);
                        isVehicleStatusWasChanged = true;
                    }
                }
                else
                {
                    throw new Exception("No vehicle was set!");
                }
            }
            catch (ValueOutOfRangeException vorex)
            {
                Console.WriteLine(vorex.Message);
                Console.WriteLine("value are between {0} or {1}", vorex.MinValue, vorex.MaxValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (isVehicleStatusWasChanged)
            {
                Console.WriteLine("Vehicle status was changed.");
            }
        }

        private void fillAirPressure()
        {
            menuChoiceTitle("Fill Air Pressure");
            bool isFillAirWasSuccessful = true;
            bool isVehcleWasSet = false;
            try
            {
                isVehcleWasSet = setLicenseNumber();
                if (isVehcleWasSet)
                {
                    CurrentGarageAction.TryAddMaxAirToWheels();
                }
                else
                {
                    isFillAirWasSuccessful = false;
                }
            }
            catch (Exception ex)
            {
                isFillAirWasSuccessful = false;
                Console.WriteLine(ex.Message);
            }

            if (isFillAirWasSuccessful)
            {
                Console.WriteLine("Air Filled Successfully");
            }
        }

        private bool setLicenseNumber()
        {
            string licenseNumber = string.Empty;
            const bool v_IsVehicleWasSet = true;
            Console.Write("Please enter vehicle license number (q for back to menu): ");
            licenseNumber = Console.ReadLine();
            if (licenseNumber == "q")
            {
                return !v_IsVehicleWasSet;
            }

            while (!CurrentGarageAction.SetCurrentVehicleInGarage(licenseNumber))
            {
                Console.WriteLine("Vehicle does not exists!");
                Console.Write("Please enter vehicle license number: ");
                licenseNumber = Console.ReadLine();
                if (licenseNumber == "q")
                {
                    return !v_IsVehicleWasSet;
                }
            }

            return v_IsVehicleWasSet;
        }

        private void loadEnergy()
        {
            menuChoiceTitle("Load Energy");
            const int k_EnergyLoaded = 2;
            bool isVehicleWasSet = false;
            isVehicleWasSet = setLicenseNumber();
            if (isVehicleWasSet)
            {
                Dictionary<int, string> energyProperties = CurrentGarageAction.GetEnergyProperties();
                bool isValid = false;

                for (int i = 1; i <= energyProperties.Count; i++)
                {
                    isValid = false;
                    Console.WriteLine(energyProperties[i]);
                    do
                    {
                        try
                        {
                            CurrentGarageAction.SetEnergyProperty(i, Console.ReadLine());

                            if (i == k_EnergyLoaded)
                            {
                                Console.WriteLine("Energy was Loaded successfully.");
                            }

                            isValid = true;
                        }
                        catch (ValueOutOfRangeException vorex)
                        {
                            Console.WriteLine(vorex.Message);
                            Console.WriteLine("value are between {0} to {1}.", vorex.MinValue, vorex.MaxValue);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    while (!isValid);
                }
            }
        }

        private void showVehicleDetails()
        {
            bool isCarWasNotSet = false;
            menuChoiceTitle("Show Vehicle Details");
            try
            {
                isCarWasNotSet = setLicenseNumber();
                if (isCarWasNotSet)
                {
                    Console.WriteLine(CurrentGarageAction.GetVehicleDetails());
                }
                else
                {
                    throw new ArgumentException("No Vehicle was set!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            askForBackToMainMenu();
        }

        private void askForBackToMainMenu()
        {
            bool isUserWantBackToMenu = false;
            Console.WriteLine("Please enter q back to main menu: ");
            while (!isUserWantBackToMenu)
            {
                isUserWantBackToMenu = Console.ReadLine() == "q";
            }
        }

        private void menuChoiceTitle(string i_headTitle)
        {
            int length = i_headTitle.Length;
            Console.Clear();
            printGarageTitle();
            Console.WriteLine("{0}{1}", Environment.NewLine, i_headTitle);
            Console.WriteLine(new string('=', length + 1));
        }

        private void printMainMenuOptions()
        {
            Console.Clear();
            Console.Write(
@"
=====================================
Garage Menu
=============
1 - Add Vehicle to the garage.
2 - Show all vehicles at the garage.
3 - Change vehicle status.
4 - Fill wheels air pressure.
5 - Load Energy.
6 - Show vehicle details.
7 - Exit.
press: ");
        }

        private void printGarageTitle()
        {
            Console.WriteLine(
@"
=========================");
        }
    }
}
