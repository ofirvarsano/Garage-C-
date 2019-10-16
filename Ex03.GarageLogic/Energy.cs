using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Energy
    {
        public enum eEnergyProperties
        {
            CurrentEnergy = 1
        }

        private readonly float r_MaxEnergy;
        private float m_CurrentEnergy;

        public float MaxEnergy
        {
            get
            {
                return r_MaxEnergy;
            }
        }

        public float CurrentEnergy
        {
            get
            {
                return m_CurrentEnergy;
            }

            set
            {
                if (m_CurrentEnergy + value <= r_MaxEnergy)
                {
                    m_CurrentEnergy += value;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, r_MaxEnergy - m_CurrentEnergy, "Out of range this amount left");
                }
            }
        }

        public float EnergyPercentLeft
        {
            get
            {
                return (m_CurrentEnergy * 100) / r_MaxEnergy;
            }
        }

        internal Energy(float i_MaxEnergy)
        {
            r_MaxEnergy = i_MaxEnergy;
        }

        public virtual void SetProperty(int i_Property, string i_InputFromUserStr)
        {
            float inputFromUserFloat = 0;
            eEnergyProperties property = (eEnergyProperties)i_Property;

            switch (property)
            {
                case eEnergyProperties.CurrentEnergy:
                    if (float.TryParse(i_InputFromUserStr, out inputFromUserFloat) && inputFromUserFloat >= 0)
                    {
                        CurrentEnergy = inputFromUserFloat;
                    }
                    else
                    {
                        throw new FormatException("You have entered wrong input!");
                    }

                    break;
            }
        }

        public virtual Dictionary<int, string> GetEnergyProperties()
        {
            Dictionary<int, string> properties = new Dictionary<int, string>();
            properties.Add(1, "Enter current energy: ");
            return properties;
        }
    }
}