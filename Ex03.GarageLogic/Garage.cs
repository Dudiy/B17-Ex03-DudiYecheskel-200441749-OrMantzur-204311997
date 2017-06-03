﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<int, VehicleInGarage> m_VehiclesInGarage = new Dictionary<int, VehicleInGarage>();

        public void AddVehicleToGarage(string i_OwnerName, string i_OwnerPhone, Vehicle i_Vehicle)
        {
            VehicleInGarage newVehicle = new VehicleInGarage(i_OwnerName, i_OwnerPhone, i_Vehicle);
            m_VehiclesInGarage.Add(i_Vehicle.GetHashCode(), newVehicle);
        }

        public bool LicensePlateExists(string i_LicensePlate)
        {
            return m_VehiclesInGarage.ContainsKey(i_LicensePlate.GetHashCode());
        }

        public void SetVehicleInGarageStatus(string i_LicensePlate, eVehicleStatus i_VehicleStatus)
        {
            m_VehiclesInGarage[i_LicensePlate.GetHashCode()].Status = i_VehicleStatus;
        }
    }
}