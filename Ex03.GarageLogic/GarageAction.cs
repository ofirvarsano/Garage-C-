using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GarageAction
    {
        private readonly Dictionary<string, VehicleInGarage> r_VehiclesInGarage = new Dictionary<string, VehicleInGarage>();
        private VehicleInGarage m_CurrentVehicleInGarage;

        public bool SetCurrentVehicleInGarage(string i_LicenseNumber)
        {
            bool isInGarage = false;
            if (IsLicenseInGarage(i_LicenseNumber))
            {
                m_CurrentVehicleInGarage = r_VehiclesInGarage[i_LicenseNumber];
                isInGarage = true;
            }

            return isInGarage;
        }

        public void AddCarToGarage(string i_OwnerName, string i_OwnerPhone, string i_LicenseNumber, int i_VehicleType)
        {
            Vehicle newVehicle;
            CreateVehicle.eVehicleType vehicleType = (CreateVehicle.eVehicleType)i_VehicleType;
            if (IsLicenseInGarage(i_LicenseNumber))
            {
                throw new ArgumentException("The vehicle already exists in the garage!");
            }
            else
            {
                newVehicle = CreateVehicle.createVehicle(vehicleType, i_LicenseNumber);
                m_CurrentVehicleInGarage = new VehicleInGarage(i_OwnerName, i_OwnerPhone, newVehicle);
                r_VehiclesInGarage.Add(i_LicenseNumber, m_CurrentVehicleInGarage);
            }
        }

        public bool CheckIfVehicleExistAndChangeStatus(string i_LicenseNumber, out string o_MessageToUser)
        {
            o_MessageToUser = string.Empty;
            bool isVehicleExists = IsLicenseInGarage(i_LicenseNumber);

            if (isVehicleExists)
            {
                m_CurrentVehicleInGarage = r_VehiclesInGarage[i_LicenseNumber];
                m_CurrentVehicleInGarage.VehicleStatus = VehicleInGarage.eVehicleStatus.InRepair;
                o_MessageToUser = "The vehicle already exists in the garage. Status changed to In Repair";
            }
            else
            {
                o_MessageToUser = "New Vehicle was Added";
            }

            return !isVehicleExists;
        }

        public bool IsLicenseInGarage(string i_LicenseNumber)
        {
            return r_VehiclesInGarage.ContainsKey(i_LicenseNumber);
        }

        public string GetListOfVehiclesInGarage()
        {
            StringBuilder listOfVehiclesInGarage = new StringBuilder();

            if (r_VehiclesInGarage.Count > 0)
            {
                int index = 1;
                listOfVehiclesInGarage.AppendFormat("List Of All Vehicles by License:{0}", Environment.NewLine);
                foreach (VehicleInGarage currentVehicle in r_VehiclesInGarage.Values)
                {
                    listOfVehiclesInGarage.AppendFormat("{0} - {1}{2}", index, currentVehicle.OwnerVehicle.LicenseNumber, Environment.NewLine);
                    index++;
                }

                listOfVehiclesInGarage.AppendFormat("{0}", Environment.NewLine);
            }
            else
            {
                listOfVehiclesInGarage.AppendLine("The garage is empty!");
            }

            return listOfVehiclesInGarage.ToString();
        }

        public string GetListOfVehiclesInGarageByStatus(int i_VehicleStatus)
        {
            StringBuilder listOfVehiclesInGarage = new StringBuilder();
            VehicleInGarage.eVehicleStatus vehicleStatus;
            int index = 1;
            if (r_VehiclesInGarage.Count > 0)
            {
                if (isUserEnumChoiceLegal<VehicleInGarage.eVehicleStatus>(i_VehicleStatus))
                {
                    vehicleStatus = (VehicleInGarage.eVehicleStatus)i_VehicleStatus;
                    listOfVehiclesInGarage.AppendFormat("List Of All Vehicles by Status: {0}", Environment.NewLine);
                    foreach (VehicleInGarage vehicleInGarage in r_VehiclesInGarage.Values)
                    {
                        if (vehicleInGarage.VehicleStatus == vehicleStatus)
                        {
                            listOfVehiclesInGarage.AppendFormat("{0} - {1}{2}", index, vehicleInGarage.OwnerVehicle.LicenseNumber, Environment.NewLine);
                            index++;
                        }
                    }
                }
                else
                {
                    throw new ValueOutOfRangeException(1, Enum.GetNames(typeof(VehicleInGarage.eVehicleStatus)).Length, "Out of range value!");
                }
            }
            else
            {
                listOfVehiclesInGarage.AppendLine("The garage is empty!");
            }

            return listOfVehiclesInGarage.ToString();
        }

        public void TryChangeVehicleStatus(int i_NewStatus)
        {
            if (m_CurrentVehicleInGarage != null)
            {
                if (isUserEnumChoiceLegal<VehicleInGarage.eVehicleStatus>(i_NewStatus))
                {
                    m_CurrentVehicleInGarage.VehicleStatus = (VehicleInGarage.eVehicleStatus)i_NewStatus;
                }
                else
                {
                    throw new ValueOutOfRangeException(1, Enum.GetNames(typeof(VehicleInGarage.eVehicleStatus)).Length, "Out of range value!");
                }
            }
            else
            {
                throw new ArgumentException("No vehicle was set!");
            }
        }

        public void TryAddMaxAirToWheels()
        {
            if (m_CurrentVehicleInGarage != null)
            {
                m_CurrentVehicleInGarage.OwnerVehicle.AddMaxAirToWheels();
            }
            else
            {
                throw new ArgumentException("No vehicle was set!");
            }
        }

        public Dictionary<int, string> GetEnergyProperties()
        {
            Dictionary<int, string> properties = null;
            if (m_CurrentVehicleInGarage != null)
            {
                properties = m_CurrentVehicleInGarage.OwnerVehicle.EnergySystem.GetEnergyProperties();
            }
            else
            {
                throw new ArgumentException("No vehicle was set!");
            }

            return properties;
        }

        public void SetEnergyProperty(int i_Property, string i_InputFromUser)
        {
            if (m_CurrentVehicleInGarage != null)
            {
                m_CurrentVehicleInGarage.OwnerVehicle.EnergySystem.SetProperty(i_Property, i_InputFromUser);
            }
            else
            {
                throw new ArgumentException("No vehicle was set!");
            }
        }

        public Dictionary<int, string> GetVehicleProperties()
        {
            Dictionary<int, string> vehicleProperties = null;
            if (m_CurrentVehicleInGarage != null)
            {
                vehicleProperties = m_CurrentVehicleInGarage.OwnerVehicle.GetVehicleProperties();
            }
            else
            {
                throw new ArgumentException("No vehicle was set!");
            }

            return vehicleProperties;
        }

        public void SetProperty(int i_Property, string i_InputFromUser)
        {
            if (m_CurrentVehicleInGarage != null)
            {
                m_CurrentVehicleInGarage.OwnerVehicle.SetProperty(i_Property, i_InputFromUser);
            }
            else
            {
                throw new ArgumentException("No vehicle was set!");
            }
        }

        public void AddWheels(string i_Manufacturer, float i_CurrentAirPressuer)
        {
            if (m_CurrentVehicleInGarage != null)
            {
                m_CurrentVehicleInGarage.OwnerVehicle.AddWheels(i_Manufacturer, i_CurrentAirPressuer);
            }
            else
            {
                throw new ArgumentException("No vehicle was set!");
            }
        }

        public string GetTypesOfVehicles()
        {
            StringBuilder stringBuilder = new StringBuilder();
            int index = 1;
            foreach (CreateVehicle.eVehicleType currentType in Enum.GetValues(typeof(CreateVehicle.eVehicleType)))
            {
                stringBuilder.AppendFormat("{0} - {1}{2}", index.ToString(), currentType.ToString(), Environment.NewLine);
                index++;
            }

            return stringBuilder.ToString();
        }

        public string GetListOfStatus()
        {
            StringBuilder stringBuilder = new StringBuilder();
            int index = 1;
            foreach (VehicleInGarage.eVehicleStatus currentStatus in Enum.GetValues(typeof(VehicleInGarage.eVehicleStatus)))
            {
                stringBuilder.AppendFormat("{0} - {1}{2}", index.ToString(), currentStatus.ToString(), Environment.NewLine);
                index++;
            }

            return stringBuilder.ToString();
        }

        public bool IsUserStatusChoiceLegal(int i_UserStatusChoice)
        {
            return isUserEnumChoiceLegal<VehicleInGarage.eVehicleStatus>(i_UserStatusChoice);
        }

        public bool IsUserVehicleTypeChoiceLegal(int i_UserVehicleTypeChoice)
        {
            return isUserEnumChoiceLegal<CreateVehicle.eVehicleType>(i_UserVehicleTypeChoice);
        }

        private bool isUserEnumChoiceLegal<T>(int i_EnumChoice)
        {
            return Enum.IsDefined(typeof(T), i_EnumChoice);
        }

        public string GetVehicleDetails()
        {
            string details = null;

            if (m_CurrentVehicleInGarage != null)
            {
                details = m_CurrentVehicleInGarage.ToString();
            }
            else
            {
                throw new ArgumentException("No vehicle was set!");
            }

            return details;
        }
    }
}
