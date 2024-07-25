using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle 
    {
        bool m_TransportsHazardousMaterials;
        float m_CargoVolume;

        internal override void PrintFullInfo()
        {
            base.PrintFullInfo();
            string convertedHazardousMaterials = m_TransportsHazardousMaterials ? "Yes" : "No";
            Console.WriteLine($"Transports Hazardou sMaterials: {convertedHazardousMaterials}");
            Console.WriteLine($"Cargo volume: {m_CargoVolume}");
            m_Engine.PrintFullInfo();
            m_Wheels.First().PrintFullInfo();
        }

        public Truck(Engine engine) 
        {
            if (engine is GasEngine fuelEngine)
            {
                base.m_Engine = engine;
                base.m_NumOfWheels = 14;
                base.ClearWheels();
                for (int i = 0; i < base.m_NumOfWheels; i++)
                {
                    Wheel wheel = new Wheel();
                    wheel.MaxAirPressure = 26;
                    base.AddWheel(wheel);
                }

                base.m_Engine = fuelEngine;
            }
            else
            {
                throw new ArgumentException("A truck engine runs on fuel.");
            }
        }

        public float CargoVolume
        {
            get { return m_CargoVolume; }
            set
            {
                if (value >= 0 )
                {
                    m_CargoVolume = value;
                }
                else
                {
                    throw new FormatException("The cargo volume must be a positive number");
                }
            }
        }

        public override List<string> GetFieldsNames()
        {
            List<string> truckData = new List<string>();
            truckData.Add("Transports Hazardous Materials\n1. Yes\n2. No");
            truckData.Add("Cargo Volume");
            m_Engine.GetEngineFieldsNames(truckData);

            return truckData;
        }

        public override void SetUniqueFields(List<string> i_UniqueFieldsValues)
        {
            if (i_UniqueFieldsValues[0] != "1" && i_UniqueFieldsValues[0] != "2")
            {
                throw new FormatException("Incorrect Transports Hazardous Materials input.");
            }
            
            m_TransportsHazardousMaterials = (i_UniqueFieldsValues[0] == "1");
            if (!float.TryParse(i_UniqueFieldsValues[1], out float volume))
            {
                throw new FormatException("Incorrect input for cargo volume.");
            }

            CargoVolume = volume;
            m_Engine.setUniqueEngineFields(i_UniqueFieldsValues[2]);

        }
    }
}
