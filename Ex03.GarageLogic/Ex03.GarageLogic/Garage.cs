using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, VehicleWrapper> m_GarageVehicles = new Dictionary<string, VehicleWrapper>();

        public void ShowInfo(string i_LicenseNumber)
        {
            Console.WriteLine($"Printing car {i_LicenseNumber} full information:");
            Console.WriteLine("=====================================================");
            Console.WriteLine($"Owner's name: {m_GarageVehicles[i_LicenseNumber].OwnerName}");
            Console.WriteLine($"Owner's phone number: {m_GarageVehicles[i_LicenseNumber].OwnerPhoneNumber}");
            Console.WriteLine($"Car's condition: {m_GarageVehicles[i_LicenseNumber].VehicleCondition}");
            m_GarageVehicles[i_LicenseNumber].Vehicle.PrintFullInfo();
        }

        public bool isExist(string i_LicenseNumber)
        {
            return m_GarageVehicles.ContainsKey(i_LicenseNumber);
        }

        public int NumOfVehicles
        {
            get { return m_GarageVehicles.Count; }
        }

        public void InflateToMax(string i_LicenseNumber)
        {
            m_GarageVehicles[i_LicenseNumber].Vehicle.CurrentAirPressureWheels = m_GarageVehicles[i_LicenseNumber].Vehicle.GetMaxAirPressure();
        }

        public void SetNewCondition(eVehicleCondition i_NewVehicleCondition, string i_LicenseNumber)
        {
            m_GarageVehicles[i_LicenseNumber].VehicleCondition = i_NewVehicleCondition;
        }

        public float GetTyrePressure(string i_LicenseNumber)
        {
            return m_GarageVehicles[i_LicenseNumber].Vehicle.CurrentAirPressureWheels;
        }

        public eVehicleCondition GetCondition(string i_LicenseNumber)
        {
            return m_GarageVehicles[i_LicenseNumber].VehicleCondition;
        }

        public void AddNewVehicle(Vehicle i_NewVehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            VehicleWrapper newVehicle = new VehicleWrapper(i_NewVehicle, i_OwnerName, i_OwnerPhoneNumber);
            m_GarageVehicles[i_NewVehicle.LicenseNumber] = newVehicle;
        }

        public void ChangeVehicleCondition(string i_LicenseNumber, eVehicleCondition i_NewVehicleCondition)
        {
            m_GarageVehicles[i_LicenseNumber].VehicleCondition = i_NewVehicleCondition;
        }

        public void PrintVehiclesByCondition(eVehicleCondition i_VehicleCondition)
        {
            if (m_GarageVehicles.Count == 0)
            {
                Console.WriteLine("There are no vehicles in the garage yet.");
            }
            else
            {
                int i = 0;

                foreach (VehicleWrapper vehicleInGarage in m_GarageVehicles.Values)
                {
                    if (i_VehicleCondition == eVehicleCondition.All || vehicleInGarage.VehicleCondition == i_VehicleCondition)
                    {
                        if (i_VehicleCondition != eVehicleCondition.All && i == 0)
                        {
                            Console.WriteLine($"Vehicles by condition: {i_VehicleCondition}");
                        }

                        Console.WriteLine($"{++i}. {vehicleInGarage.Vehicle.LicenseNumber}");
                    }
                }

                if (i == 0)
                {
                    Console.WriteLine($"There are no vehicle in the garage by condition of {i_VehicleCondition}");
                }
            }
        }

        public void AddEnergy(string i_LicenseNumber, float i_AmountToFuel)
        {
            m_GarageVehicles[i_LicenseNumber].Vehicle.Engine.EnergyFilling(i_AmountToFuel);
        }

        public bool HasElectricEngine(string i_LicenseNumber)
        {
            return m_GarageVehicles[i_LicenseNumber].Vehicle.Engine is ElectricEngine;
        }

        public void CheckValidFuel(string i_LicenseNumber, GasEngine.eFuelType i_FuelType)
        {
            if (m_GarageVehicles[i_LicenseNumber].Vehicle.Engine is GasEngine gasEngine && gasEngine.FuelType != i_FuelType)
            {
                throw new ArgumentException($"Invalid Fuel Type entered, Car {i_LicenseNumber} uses {gasEngine.FuelType}");
            }
        }

        public bool HasGasEngine(string i_LicenseNumber)
        {
            return m_GarageVehicles[i_LicenseNumber].Vehicle.Engine is GasEngine;
        }

        public float MaxEnergy(string i_LicenseNumber)
        {
            return m_GarageVehicles[i_LicenseNumber].Vehicle.Engine.MaximumEnergyAmount;
        }

        public float CurrentEnergy(string i_LicenseNumber)
        {
            return m_GarageVehicles[i_LicenseNumber].Vehicle.Engine.CurrentEnergyQuantity;
        }

        class VehicleWrapper
        {
            Vehicle m_Vehicle;
            readonly string r_VehicleOwnerName, r_VehicleOwnerPhoneNumber;
            eVehicleCondition m_VehicleCondition;

            public VehicleWrapper(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
            {
                m_VehicleCondition = eVehicleCondition.Repairing;
                m_Vehicle = i_Vehicle;
                r_VehicleOwnerName = i_OwnerName;
                r_VehicleOwnerPhoneNumber = i_OwnerPhoneNumber;
            }

            public string OwnerName
            {
                get { return r_VehicleOwnerName; }
            }

            public string OwnerPhoneNumber
            {
                get { return r_VehicleOwnerPhoneNumber; }
            }

            public Vehicle Vehicle
            {
                get { return m_Vehicle; }
            }

            public eVehicleCondition VehicleCondition
            {
                get { return m_VehicleCondition; }
                set { m_VehicleCondition = value; }
            }
        }

        public enum eVehicleCondition
        {
            Repairing = 1,
            Repaired,
            Paid,
            All
        }
    }
}
