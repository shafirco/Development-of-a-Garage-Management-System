using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        eLicenseType m_LicenseType;
        int m_EngineVolume;

        internal override void PrintFullInfo()
        {
            base.PrintFullInfo();
            Console.WriteLine($"License type: {m_LicenseType}");
            Console.WriteLine($"Engine volume: {m_EngineVolume}");
            m_Engine.PrintFullInfo();
            m_Wheels.First().PrintFullInfo();
        }

        public override List<string> GetFieldsNames()
        {
            List<string> motorcycleData = new List<string>();

            motorcycleData.Add("License Type:\n1. A1\n2. A2\n3. AA\n4. B1");
            motorcycleData.Add("Engine Volume");
            m_Engine.GetEngineFieldsNames(motorcycleData);

            return motorcycleData;
        }

        public override void SetUniqueFields(List<string> i_UniqueFieldsValues)
        {
            if (!int.TryParse(i_UniqueFieldsValues[0], out int licenseType))
            {
                throw new FormatException("invalid license type chosen.");
            }
            else if (!Enum.IsDefined(typeof(eLicenseType), licenseType))
            {
                throw new ArgumentException("Invalid license type. Please enter a valid license type.");
            }

            this.LicenseType = (eLicenseType)licenseType;
            if (!int.TryParse(i_UniqueFieldsValues[1], out int volume))
            {
                throw new FormatException("invalid engine volume chosen.");
            }

            EngineVolume = volume;
            m_Engine.setUniqueEngineFields(i_UniqueFieldsValues[2]);

        }

        public Motorcycle(Engine engine)
        {
            base.m_NumOfWheels = 2;

            for (int i = 0; i < base.m_NumOfWheels; i++)
            {
                Wheel wheel = new Wheel();
                wheel.MaxAirPressure = 31;
                base.AddWheel(wheel);
            }

            if (engine is ElectricEngine batteryEngine)
            {
                m_Engine = batteryEngine;
            }
            else if (engine is GasEngine fuelEngine)
            {
                m_Engine = fuelEngine;
            }
        }

        public int EngineVolume
        {
            get { return m_EngineVolume; }
            set
            {
                if (value >0)
                {
                    m_EngineVolume = value;
                }
                else
                {
                    throw new FormatException("The engine volume must be a positive number");
                }
            }

        }

        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }
        public enum eLicenseType
        {
            A1 = 1,
            A2,
            AA,
            B1,
        }
    }
    
}
