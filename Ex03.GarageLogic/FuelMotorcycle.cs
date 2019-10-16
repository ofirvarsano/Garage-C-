using System.Text;

namespace Ex03.GarageLogic
{
    internal class FuelMotorcycle : Motorcycle
    {
        private const float k_MaxWheelAirPressure = 28;
        private const int k_NumberOfWheels = 2;
        private const float k_MaxFuelTank = 5.5f;
        private const FuelEnergy.eFuelKind k_FuelKind = FuelEnergy.eFuelKind.Octan95;

        internal FuelMotorcycle(string i_LicenseNumber)
            : base(i_LicenseNumber, k_MaxWheelAirPressure, k_NumberOfWheels)
        {
            EnergySystem = new FuelEnergy(k_MaxFuelTank, k_FuelKind);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Vehicle type: Fuel Motorcycle");
            return stringBuilder.ToString() + base.ToString();
        }
    }
}
