using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal abstract class Motorcycle : Vehicle
    {
        public enum eMotorcycleProperties
        {
            Model = 1,
            LicenseType = 2,
            EngineCapacity = 3
        }

        public enum eLicenseType
        {
            A = 1,
            A1 = 2,
            AB = 3,
            B1 = 4
        }

        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }

            set
            {
                m_LicenseType = value;
            }
        }

        public int EngineCapacity
        {
            get
            {
                return m_EngineCapacity;
            }

            set
            {
                m_EngineCapacity = value;
            }
        }

        internal Motorcycle(string i_LicenseNumber, float i_MaxWheelAirPressure, int i_NumberOfWheels)
            : base(i_LicenseNumber, i_MaxWheelAirPressure, i_NumberOfWheels)
        {
        }

        public override void SetProperty(int i_Property, string i_InputFromUser)
        {
            eMotorcycleProperties property = (eMotorcycleProperties)i_Property;
            int inputFromUserInt;

            switch (property)
            {
                case eMotorcycleProperties.Model:
                    Model = i_InputFromUser;
                    break;
                case eMotorcycleProperties.LicenseType:
                    if (int.TryParse(i_InputFromUser, out inputFromUserInt))
                    {
                        if (Enum.IsDefined(typeof(eLicenseType), inputFromUserInt))
                        {
                            LicenseType = (eLicenseType)inputFromUserInt;
                        }
                        else
                        {
                            throw new ValueOutOfRangeException(1, Enum.GetNames(typeof(eLicenseType)).Length, "You have entered out of range input!");
                        }
                    }
                    else
                    {
                        throw new FormatException("You have entered wrong input!");
                    }

                    break;
                case eMotorcycleProperties.EngineCapacity:
                    if (int.TryParse(i_InputFromUser, out inputFromUserInt) && inputFromUserInt > 0)
                    {
                        EngineCapacity = inputFromUserInt;
                    }
                    else
                    {
                        throw new FormatException("You have entered wrong input!");
                    }

                    break;
            }
        }

        public override Dictionary<int, string> GetVehicleProperties()
        {
            Dictionary<int, string> properties = new Dictionary<int, string>();
            properties.Add((int)eMotorcycleProperties.Model, "Please enter model: ");
            properties.Add((int)eMotorcycleProperties.LicenseType, Car.EnterEnumMessage<eLicenseType>("License Type:"));
            properties.Add((int)eMotorcycleProperties.EngineCapacity, "Please enter engine volume: ");
            return properties;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat(
@"LicenseType: {0}
EngineCapacity: {1}",
m_LicenseType.ToString(),
m_EngineCapacity.ToString());
            return base.ToString() + stringBuilder.ToString();
        }
    }
}
