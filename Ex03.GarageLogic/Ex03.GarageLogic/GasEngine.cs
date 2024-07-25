using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class GasEngine : Engine
    {
        readonly eFuelType r_FuelType;

        internal override void PrintFullInfo()
        {
            base.PrintFullInfo();
            Console.WriteLine($"Fuel type: {r_FuelType}");
        }

        public GasEngine(float i_MaxEnergyAmount, eFuelType i_FuelType) : base(i_MaxEnergyAmount) 
        {
            r_FuelType = i_FuelType;
        }

        public eFuelType FuelType
        {
            get { return r_FuelType; }
        }

        public override void GetEngineFieldsNames(List<string> i_FieldsNames)
        {
            i_FieldsNames.Add(string.Format("Fuel Quantity in Liters (0-{0})", base.MaximumEnergyAmount));
        }

        public enum eFuelType
        {
            Octan95 = 1,
            Octan96,
            Octan98,
            Soler,
        }
    }
}
