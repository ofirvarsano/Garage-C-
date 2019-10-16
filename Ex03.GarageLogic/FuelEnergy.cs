using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class FuelEnergy : Energy
    {
        public enum eFuelEngineProperties
        {
            FuelType = 1,
            CurrentAmount = 2
        }

        public enum eFuelKind
        {
            Octan95 = 1,
            Octan96 = 2,
            Octan98 = 3,
            Soler = 4
        }

        private readonly eFuelKind r_FuelKind;

        internal FuelEnergy(float i_MaxEnergy, eFuelKind i_FuelKind)
            : base(i_MaxEnergy)
        {
            r_FuelKind = i_FuelKind;
        }

        public override Dictionary<int, string> GetEnergyProperties()
        {
            Dictionary<int, string> properties = new Dictionary<int, string>();
            properties.Add(1, Car.EnterEnumMessage<eFuelKind>("fuel type"));
            properties.Add(2, "Please enter fuel amount: ");
            return properties;
        }

        public override void SetProperty(int i_Property, string i_InputFromUser)
        {
            eFuelEngineProperties property = (eFuelEngineProperties)i_Property;
            float inputFromUserFloat;
            int inputFromUserInt;

            switch (property)
            {
                case eFuelEngineProperties.FuelType:
                    if (int.TryParse(i_InputFromUser, out inputFromUserInt))
                    {
                        if (Enum.IsDefined(typeof(eFuelKind), inputFromUserInt))
                        {
                            if ((eFuelKind)inputFromUserInt != r_FuelKind)
                            {
                                throw new ArgumentException("Wrong fuel type!");
                            }
                        }
                        else
                        {
                            throw new ValueOutOfRangeException(1, Enum.GetNames(typeof(eFuelKind)).Length, "You have entered out of range input!");
                        }
                    }
                    else
                    {
                        throw new FormatException("You have entered wrong input!");
                    }

                    break;
                case eFuelEngineProperties.CurrentAmount:
                    if (float.TryParse(i_InputFromUser, out inputFromUserFloat) && inputFromUserFloat >= 0)
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

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Fuel Type: " + r_FuelKind.ToString());
            return stringBuilder.ToString();
        }
    }
}
