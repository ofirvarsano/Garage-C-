using System.Text;

namespace Ex03.GarageLogic
{
    internal class ElectricCar : Car
    {
        private const float k_MaxWheelAirPressure = 30;
        private const int k_NumberOfWheels = 4;
        private const float k_MaxBatteryTime = 2.5f;

        internal ElectricCar(string i_LicenseNUmber)
            : base(i_LicenseNUmber, k_MaxWheelAirPressure, k_NumberOfWheels)
        {
            EnergySystem = new ElectricEnergy(k_MaxBatteryTime);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Vehicle type: Electric Car");
            return stringBuilder.ToString() + base.ToString();
        }
    }
}
