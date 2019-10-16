using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public enum eVehicleProperties
        {
            Model = 1
        }

        private readonly List<Wheel> r_WheelsList;
        private readonly int r_NumOfWheels;
        private readonly float r_MaxWheelAirPressure;
        private string m_Model;
        private string m_LicenseNumber;
        private Energy m_Energy;

        public string Model
        {
            get
            {
                return m_Model;
            }

            set
            {
                m_Model = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }

            set
            {
                m_LicenseNumber = value;
            }
        }

        public Energy EnergySystem
        {
            get
            {
                return m_Energy;
            }

            set
            {
                m_Energy = value;
            }
        }

        public float EnergyPercentLeft
        {
            get
            {
                return m_Energy.EnergyPercentLeft;
            }
        }

        internal Vehicle(string i_LicenseNumber, float i_MaxWheelAirPressure, int i_NumOfWheels)
        {
            m_LicenseNumber = i_LicenseNumber;
            r_MaxWheelAirPressure = i_MaxWheelAirPressure;
            r_NumOfWheels = i_NumOfWheels;
            r_WheelsList = new List<Wheel>();
        }

        public void AddWheels(string i_ManufactureName, float i_CurrentAirPressure)
        {
            r_WheelsList.Clear();
            if (i_CurrentAirPressure <= r_MaxWheelAirPressure)
            {
                for (int i = 0; i < r_NumOfWheels; i++)
                {
                    Wheel wheel = new Wheel(i_ManufactureName, i_CurrentAirPressure, r_MaxWheelAirPressure);
                    r_WheelsList.Add(wheel);
                }
            }
            else
            {
                throw new ValueOutOfRangeException(1, r_MaxWheelAirPressure, "Out of range input!");
            }
        }

        public void AddAirToWheels(float i_AirToAdd)
        {
            if (r_WheelsList.Count > 0)
            {
                foreach (Wheel currentWheel in r_WheelsList)
                {
                    currentWheel.AddAirToWheel(i_AirToAdd);
                }
            }
        }

        public void AddMaxAirToWheels()
        {
            if (r_WheelsList.Count > 0)
            {
                foreach (Wheel currentWheel in r_WheelsList)
                {
                    float leftAirToAdd = currentWheel.MaxAirPressure - currentWheel.CurrentAirPressure;
                    currentWheel.AddAirToWheel(leftAirToAdd);
                }
            }
        }

        public virtual Dictionary<int, string> GetVehicleProperties()
        {
            Dictionary<int, string> properties = new Dictionary<int, string>();
            foreach (eVehicleProperties currentProperty in Enum.GetValues(typeof(eVehicleProperties)))
            {
                properties.Add((int)currentProperty, currentProperty.ToString());
            }

            return properties;
        }

        public virtual void SetProperty(int i_Property, string i_InputFromUser)
        {
            eVehicleProperties property = (eVehicleProperties)i_Property;

            switch (property)
            {
                case eVehicleProperties.Model:
                    Model = i_InputFromUser;
                    break;
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            int wheelNumber = 1;
            stringBuilder.AppendFormat(
@"LicenseNumber: {0}
ModelName: {1}
EnergyPercentage: {2}%
",
LicenseNumber,
Model,
string.Format("{0:0.00}", EnergyPercentLeft));

            foreach (Wheel currentWheel in r_WheelsList)
            {
                stringBuilder.AppendFormat(
@"Wheel number {0}: 
{1}
",
wheelNumber,
currentWheel.ToString());
                wheelNumber++;
            }

            return stringBuilder.ToString() + m_Energy.ToString();
        }
    }
}