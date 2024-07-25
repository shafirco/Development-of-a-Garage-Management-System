using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        eColorCar m_ColorCar;
        eDoorsNumber m_NumberOfDoors;

        internal override void PrintFullInfo()
        {
            base.PrintFullInfo();
            Console.WriteLine($"Color: {m_ColorCar}");
            Console.WriteLine($"Number of doors: {m_NumberOfDoors}");
            m_Engine.PrintFullInfo();
            m_Wheels.First().PrintFullInfo();
        }

        public Car(Engine engine)
        {
            base.m_NumOfWheels = 5;
            for (int i = 0; i < base.m_NumOfWheels; i++)
            {
                Wheel wheel = new Wheel();
                wheel.MaxAirPressure = 33;
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

        public override List<string> GetFieldsNames()
        {
            List<string> carData = new List<string>();

            carData.Add("color of the car (1-4)\n1. Black\n2. White\n3. Yellow\n4. Red");
            carData.Add("Number of Doors (2-5)");
            m_Engine.GetEngineFieldsNames(carData);

            return carData;
        }

        public override void SetUniqueFields(List<string> i_UniqueFieldsValues)
        {
            if (!int.TryParse(i_UniqueFieldsValues[0], out int color))
            {
                throw new FormatException("invalid color of car chosen.");
            }
            else if (!Enum.IsDefined(typeof(eColorCar), color))
            {
                throw new ArgumentException("Invalid vehicle color. Please enter a valid vehicle color.");
            }

            this.ColorCar = (eColorCar)color;
            if (!int.TryParse(i_UniqueFieldsValues[1], out int numOfDoors))
            {
                throw new FormatException("invalid number of doors of car chosen.");
            }
            else if (numOfDoors < 2 || numOfDoors > 5)
            {
                throw new ValueOutOfRangeException(5, 2);
            }

            this.NumberCarDoors = (eDoorsNumber)numOfDoors;
            m_Engine.setUniqueEngineFields(i_UniqueFieldsValues[2]);

        }

        public eColorCar ColorCar
        {
            get { return m_ColorCar; }
            set { m_ColorCar = value; }
        }

        public eDoorsNumber NumberCarDoors
        {
            get { return m_NumberOfDoors; }
            set { m_NumberOfDoors = value; }
        }

    }
    public enum eColorCar
    {
        Black = 1,
        White,
        Yellow,
        Red,
    }

    public enum eDoorsNumber
    {
        Two = 2,
        Three,
        Four,
        Five,
    }
}
