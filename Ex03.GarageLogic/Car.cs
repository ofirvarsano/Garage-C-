using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal abstract class Car : Vehicle
    {
        public enum eCarProperties
        {
            Model = 1,
            Color = 2,
            NumberOfDoors = 3
        }

        public enum eCarColor
        {
            Yellow = 1,
            White = 2,
            Red = 3,
            Black = 4
        }

        public enum eCarNumberOfDoors
        {
            TwoDoors = 1,
            ThreeDoors = 2,
            FourDoors = 3,
            FiveDoors = 4
        }

        private eCarNumberOfDoors m_NumberOfDoors;
        private eCarColor m_CarColor;

        public eCarColor CarColor
        {
            get
            {
                return m_CarColor;
            }

            set
            {
                m_CarColor = value;
            }
        }

        public eCarNumberOfDoors NumberOfDoors
        {
            get
            {
                return m_NumberOfDoors;
            }

            set
            {
                m_NumberOfDoors = value;
            }
        }

        public static string EnterEnumMessage<T>(string i_ValueName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int index = 1;
            stringBuilder.AppendFormat("Please enter {0}{1}", i_ValueName, Environment.NewLine);
            foreach (T currentValue in Enum.GetValues(typeof(T)))
            {
                stringBuilder.AppendFormat("{0} - {1}{2}", index, currentValue.ToString(), Environment.NewLine);
                index++;
            }

            return stringBuilder.ToString();
        }

        internal Car(string i_LicenseNumber, float i_MaxWheelAirPressure, int i_NumberOfWheels)
        : base(i_LicenseNumber, i_MaxWheelAirPressure, i_NumberOfWheels)
        {
        }

        public override void SetProperty(int i_Property, string i_InputFromUser)
        {
            eCarProperties property = (eCarProperties)i_Property;
            int inputFromUserInt;
            switch (property)
            {
                case eCarProperties.Model:
                    Model = i_InputFromUser;
                    break;
                case eCarProperties.Color:
                    if (int.TryParse(i_InputFromUser, out inputFromUserInt))
                    {
                        if (Enum.IsDefined(typeof(eCarColor), inputFromUserInt))
                        {
                            CarColor = (eCarColor)inputFromUserInt;
                        }
                        else
                        {
                            throw new ValueOutOfRangeException(1, Enum.GetNames(typeof(eCarColor)).Length, "You have entered out of range input!");
                        }
                    }
                    else
                    {
                        throw new FormatException("You have entered wrong input!");
                    }

                    break;
                case eCarProperties.NumberOfDoors:
                    if (int.TryParse(i_InputFromUser, out inputFromUserInt))
                    {
                        if (Enum.IsDefined(typeof(eCarNumberOfDoors), inputFromUserInt))
                        {
                            NumberOfDoors = (eCarNumberOfDoors)inputFromUserInt;
                        }
                        else
                        {
                            throw new ValueOutOfRangeException(1, Enum.GetNames(typeof(eCarNumberOfDoors)).Length, "You have entered out of range input!");
                        }
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
            properties.Add((int)eCarProperties.Model, "Please enter model: ");
            properties.Add((int)eCarProperties.Color, EnterEnumMessage<eCarColor>("car color"));
            properties.Add((int)eCarProperties.NumberOfDoors, EnterEnumMessage<eCarNumberOfDoors>("number of doors"));
            return properties;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat(
@"Car color: {0}
Number of doors: {1}
",
CarColor.ToString(),
NumberOfDoors.ToString());
            return base.ToString() + stringBuilder.ToString();
        }
    }
}
