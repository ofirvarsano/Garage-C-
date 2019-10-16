using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class ElectricEnergy : Energy
    {
        internal ElectricEnergy(float i_MaxEnergy)
            : base(i_MaxEnergy)
        {
        }

        public override Dictionary<int, string> GetEnergyProperties()
        {
            Dictionary<int, string> properties = new Dictionary<int, string>();
            properties.Add(1, "Enter current battery time: ");
            return properties;
        }

        public override string ToString()
        {
            return string.Empty;
        }
    }
}