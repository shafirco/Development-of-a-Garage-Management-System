using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaxEnergyAmount) : base(i_MaxEnergyAmount) {}

        public override void GetEngineFieldsNames(List<string> i_FieldsNames)
        {
            i_FieldsNames.Add(string.Format("the battery time remaining in hours (0-{0})", base.MaximumEnergyAmount));
        }
    }
}
