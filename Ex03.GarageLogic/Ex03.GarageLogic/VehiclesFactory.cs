using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class VehiclesFactory
    {
        public static Vehicle ProduceNewVehicle(eVehicleType i_VehicleType)
        {
            Vehicle newVehicle = null;

            switch (i_VehicleType)
            {
                case eVehicleType.RegularMotorcycle:
                    newVehicle = new Motorcycle(new GasEngine(6.4f, GasEngine.eFuelType.Octan98));
                    break;
                case eVehicleType.ElectricMotorcycle:
                    newVehicle = new Motorcycle(new ElectricEngine(2.6f));
                    break;
                case eVehicleType.RegularCar:
                    newVehicle = new Car(new GasEngine(46f, GasEngine.eFuelType.Octan96));
                    break;
                case eVehicleType.ElectricCar:
                    newVehicle = new Car(new ElectricEngine(5.2f));
                    break;
                case eVehicleType.Truck:
                    newVehicle = new Truck(new GasEngine(135f, GasEngine.eFuelType.Soler));
                    break;
            }

            return newVehicle;
        }

        public static void SetUniqueVehicleProperties(Vehicle o_NewVehicle, List<string> i_UniqueFieldsValues)
        {
            o_NewVehicle.SetUniqueFields(i_UniqueFieldsValues);
        }

        public static void SetBasicVehicleProperties(Vehicle o_NewVehicle, string i_LicenseNumber, string i_ModelName,
             string i_WheelManufacturerName, float i_WheelCurrentAirPressureInput)
        {
            o_NewVehicle.LicenseNumber = i_LicenseNumber;
            o_NewVehicle.Model = i_ModelName;
            o_NewVehicle.CurrentAirPressureWheels = i_WheelCurrentAirPressureInput;
            o_NewVehicle.ManufacturerName = i_WheelManufacturerName;
        }

        public enum eVehicleType
        {
            RegularMotorcycle = 1,
            ElectricMotorcycle,
            RegularCar,
            ElectricCar,
            Truck
        }
    }
}
