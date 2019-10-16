using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Truck : Vehicle
    {
        public enum eTruckProperties
        {
            Model = 1,
            IsTransportingHazardous = 2,
            MaxCarryWeight = 3,
            VolumeOfCargo = 4
        }

        private bool m_IsTransportingHazardous;
        private float m_VolumeOfCargo;
        private float m_MaxCarryWeight;

        public bool IsTransportingHazardous
        {
            get
            {
                return m_IsTransportingHazardous;
            }

            set
            {
                m_IsTransportingHazardous = value;
            }
        }

        public float VolumeOfCargo
        {
            get
            {
                return m_VolumeOfCargo;
            }

            set
            {
                if (value >= 0 && value <= m_MaxCarryWeight)
                {
                    m_VolumeOfCargo = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, m_MaxCarryWeight, "Value not in range!");
                }
            }
        }

        public float MaxCarryWeight
        {
            get
            {
                return m_MaxCarryWeight;
            }

            set
            {
                m_MaxCarryWeight = value;
            }
        }

        internal Truck(string i_LicenseNumber, float i_MaxWheelAirePressure, int i_NumberOfWheels)
            : base(i_LicenseNumber, i_MaxWheelAirePressure, i_NumberOfWheels)
        {
        }

        public override void SetProperty(int i_Property, string i_InputFromUser)
        {
            eTruckProperties property = (eTruckProperties)i_Property;
            int inputFromUserInt;
            float inputFromUserFloat;
            switch (property)
            {
                case eTruckProperties.Model:
                    Model = i_InputFromUser;
                    break;
                case eTruckProperties.IsTransportingHazardous:
                    if (int.TryParse(i_InputFromUser, out inputFromUserInt))
                    {
                        if (inputFromUserInt == 1)
                        {
                            IsTransportingHazardous = true;
                        }
                        else if (inputFromUserInt == 2)
                        {
                            IsTransportingHazardous = false;
                        }
                        else
                        {
                            throw new ValueOutOfRangeException(1, 2, "Please enter 1 - Yes or 2 - No");
                        }
                    }
                    else
                    {
                        throw new FormatException("You have entered wrong input!");
                    }

                    break;
                case eTruckProperties.MaxCarryWeight:
                    if (float.TryParse(i_InputFromUser, out inputFromUserFloat))
                    {
                        if (inputFromUserFloat > 0 && inputFromUserFloat <= 12000)
                        {
                            MaxCarryWeight = inputFromUserFloat;
                        }
                        else
                        {
                            throw new ValueOutOfRangeException(1, 12000, "Max Volume of Cargo if out of range");
                        }
                    }
                    else
                    {
                        throw new FormatException("You have entered wrong input!");
                    }

                    break;
                case eTruckProperties.VolumeOfCargo:
                    if (float.TryParse(i_InputFromUser, out inputFromUserFloat))
                    {
                        if (inputFromUserFloat < m_MaxCarryWeight && inputFromUserFloat >= 0)
                        {
                            m_VolumeOfCargo = inputFromUserFloat;
                        }
                        else
                        {
                            throw new ValueOutOfRangeException(0, m_MaxCarryWeight, "Volume if cargo Out of range");
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
            properties.Add((int)eTruckProperties.Model, "Please enter model: ");
            properties.Add((int)eTruckProperties.IsTransportingHazardous, "Carry dangerous materials ? 1 - Yes or 2 - No: ");
            properties.Add((int)eTruckProperties.MaxCarryWeight, "Please enter max carry weight: ");
            properties.Add((int)eTruckProperties.VolumeOfCargo, "Please enter current carry weight: ");
            return properties;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat(
@"Max carry weight: {0}
Contains dangerous goods: {1}
Current carry weight: {2}
",
m_MaxCarryWeight.ToString(),
m_IsTransportingHazardous.ToString(),
m_VolumeOfCargo);
            return base.ToString() + stringBuilder.ToString();
        }
    }
}
