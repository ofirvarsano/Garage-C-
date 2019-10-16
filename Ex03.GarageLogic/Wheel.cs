using System.Text;

namespace Ex03.GarageLogic
{
    internal class Wheel
    {
        private readonly string r_Manufacturer;
        private readonly float r_MaxAirPressure;
        private float m_CurrentAirPressure;

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            private set
            {
                m_CurrentAirPressure = value;
            }
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }

        public string Manufacturer
        {
            get
            {
                return r_Manufacturer;
            }
        }

        internal Wheel(string i_Manufacturer, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            r_Manufacturer = i_Manufacturer;
            m_CurrentAirPressure = i_CurrentAirPressure;
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public bool AddAirToWheel(float i_AirToAdd)
        {
            string exceptionMessage = null;
            bool isInRange = true;
            if (i_AirToAdd >= 0)
            {
                if (i_AirToAdd + CurrentAirPressure <= MaxAirPressure)
                {
                    CurrentAirPressure += i_AirToAdd;
                }
                else
                {
                    exceptionMessage = "You have added more air than need, it is unsafe";
                    isInRange = false;
                }
            }
            else
            {
                exceptionMessage = "You don't need take the air out";
                isInRange = false;
            }

            if (!isInRange)
            {
                throw new ValueOutOfRangeException(0, r_MaxAirPressure, exceptionMessage);
            }

            return isInRange;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat(
@"ManufacturerName: {0}
AirPressure: {1}",
Manufacturer,
CurrentAirPressure.ToString());
            return stringBuilder.ToString();
        }
    }
}