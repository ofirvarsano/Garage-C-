using System.Text;

namespace Ex03.GarageLogic
{
    internal class ElectricMotorcycle : Motorcycle
    {
        private const float k_MaxWheelAirPressure = 28;
        private const int k_NumberOfWheels = 2;
        private const float k_MaxBatteryTime = 1.6f;

        internal ElectricMotorcycle(string i_LicenseNumber)
            : base(i_LicenseNumber, k_MaxWheelAirPressure, k_NumberOfWheels)
        {
            EnergySystem = new ElectricEnergy(k_MaxBatteryTime);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Vehicle type: Electric Motorcycle");
            return stringBuilder.ToString() + base.ToString();
        }
    }
}
