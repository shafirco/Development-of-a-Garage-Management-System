using System;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        string m_ManufacturerName;
        float m_CurrentAirPressure;
        float m_MaximumAirPressure;

        internal void PrintFullInfo()
        {
            Console.WriteLine("Wheels info:");
            Console.WriteLine($"Manufacturer Wheels' Name: {m_ManufacturerName}");
            Console.WriteLine($"Wheels' current pressure: {m_CurrentAirPressure}");
            Console.WriteLine($"Wheels' maximum pressure: {m_MaximumAirPressure}");
        }

        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Manufacturer's name cannot be null or empty.");
                }

                m_ManufacturerName = value;
            }
        }

        public float AirPressure
        {
            get { return m_CurrentAirPressure; }
            set 
            {
                if ((value <= m_MaximumAirPressure) && (value > 0))
                {
                    m_CurrentAirPressure = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(m_MaximumAirPressure, 0);
                }
            }
        }

        public float MaxAirPressure
        {
            get { return m_MaximumAirPressure; }
            set
            {
                if (value > 0)
                {
                    m_MaximumAirPressure = value;
                }
                else
                {
                    throw new FormatException("The max air pressure must be a positive number");
                }

            }
        }
    }
}
