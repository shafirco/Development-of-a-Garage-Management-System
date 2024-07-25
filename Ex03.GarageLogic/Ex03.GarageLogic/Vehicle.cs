using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_LicenseNumber, m_Model;
        protected List<Wheel> m_Wheels = new List<Wheel>();
        protected int m_NumOfWheels;
        protected Engine m_Engine;

        internal virtual void PrintFullInfo()
        {
            Console.WriteLine($"License number: {m_LicenseNumber}");
            Console.WriteLine($"Model: {m_Model}");
            Console.WriteLine($"Number of wheels: {m_NumOfWheels}");
        }

        internal Engine Engine
        {
            get { return m_Engine; }
        }

        public void AddWheel(Wheel i_Wheel)
        {
            m_Wheels.Add(i_Wheel);
        }

        public float GetMaxAirPressure()
        {
            return m_Wheels.First().MaxAirPressure;
        }

        public float CurrentAirPressureWheels
        {
            get { return m_Wheels.First().AirPressure; }
            set
            {
                foreach (Wheel wheel in m_Wheels)
                {
                    wheel.AirPressure = value;
                }
            }
        }

        public string ManufacturerName
        {
            get { return m_Wheels.First().ManufacturerName; }
            set
            {
                foreach (Wheel wheel in m_Wheels)
                {
                    wheel.ManufacturerName = value;
                }
            }
        }

        public void ClearWheels()
        {
            m_Wheels.Clear();
        }

        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
            set 
            { 
                if (value == null)
                {
                    throw new ArgumentException("No entered license number");
                }
                if (value.All(char.IsDigit))
                {
                    m_LicenseNumber = value;
                }
                else
                {
                    throw new FormatException("License number contains only digits");
                }
            }
        }

        public string Model
        {
            get { return m_Model; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Vehicle's model cannot be null or empty.");
                }
                else if (!int.TryParse(value, out int model))
                {
                    throw new FormatException("Invalid model, please enter only numbers");
                }
                else if (model > DateTime.Now.Year)
                {
                    throw new FormatException("Invalid model, model cannot be in the future.");
                }

                m_Model = value;
            }
        }

        public abstract List<string> GetFieldsNames();

        public abstract void SetUniqueFields(List<string> i_UniqueFieldsValues);

    }
}
