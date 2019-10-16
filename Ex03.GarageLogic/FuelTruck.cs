using System.Text;

namespace Ex03.GarageLogic
{
    internal class FuelTruck : Truck
    {
        private const float k_MaxWheelAirPressure = 26;
        private const int k_NumberOfWheels = 16;
        private const float k_MaxFuelTank = 120;
        private const FuelEnergy.eFuelKind k_FuelKind = FuelEnergy.eFuelKind.Soler;

        internal FuelTruck(string i_LicenseNumber)
            : base(i_LicenseNumber, k_MaxWheelAirPressure, k_NumberOfWheels)
        {
            EnergySystem = new FuelEnergy(k_MaxFuelTank, k_FuelKind);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Vehicle type: Fuel Truck");
            return stringBuilder.ToString() + base.ToString();
        }
    }
}
