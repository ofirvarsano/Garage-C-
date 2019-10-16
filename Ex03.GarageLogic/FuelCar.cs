using System;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class FuelCar : Car
    {
        private const float k_MaxWheelAirPressure = 30;
        private const int k_NumberOfWheels = 4;
        private const float k_MaxFuelTank = 42;
        private const FuelEnergy.eFuelKind k_FuelKind = FuelEnergy.eFuelKind.Octan96;

        internal FuelCar(string i_LicenseNUmber)
            : base(i_LicenseNUmber, k_MaxWheelAirPressure, k_NumberOfWheels)
        {
            EnergySystem = new FuelEnergy(k_MaxFuelTank, k_FuelKind);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("Vehicle type: Fuel Car{0}", Environment.NewLine);
            return stringBuilder.ToString() + base.ToString();
        }
    }
}
