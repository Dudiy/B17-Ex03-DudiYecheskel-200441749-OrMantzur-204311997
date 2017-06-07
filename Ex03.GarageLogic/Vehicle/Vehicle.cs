﻿/*
 * Instructions for inheritants:
 *  -   all inheritants must implement "initUserInputFunctions" which populates the static Dictionary "s_SetFunctionsForAddedParams"
 *      the dictionary will hold <description, function name> for all functions that will be used in order to set the data members
 *      added to Vehicle by the new inheritant class
 *  -   inheritants ctor 
 *      -   update value of k_MaxWheelAirPress
 *      -   initialize the m_Wheels list according to the number and type of wheels needed
 *      -   updates m_Engine to the correct engine
 * 
 */
using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string m_LicensePlate;
        private readonly string m_ModelName = "No model name entered";
        protected float k_MaxWheelAirPress = 0;
        protected List<Wheel> m_Wheels = new List<Wheel>();
        protected Engine m_Engine = new MotorEngine(0, eFuelType.Octan95);
        protected static Dictionary<string, string> s_SetFunctionsForAddedParams = null;

        // assumption, input parameters are validated before calling the ctor        
        internal Vehicle(string i_LicensePlate, string i_ModelName)
        {
            m_LicensePlate = i_LicensePlate;
            m_ModelName = i_ModelName;
            initUserInputFunctions();
        }

        // all inheritants must provide a function to initialize a list of all user input functions
        protected abstract void initUserInputFunctions();

        // ==================================================== Propeties ====================================================
        public string LicensePlate
        {
            get { return m_LicensePlate; }
        }

        public string ModelName
        {
            get { return m_ModelName; }
        }

        public string WheelManufacturer
        {
            get { return m_Wheels[0].Manufacturer; }
            set
            {
                foreach (Wheel wheel in m_Wheels)
                {
                    wheel.Manufacturer = value;
                }
            }
        }

        public Type EngineType
        {
            get { return m_Engine.GetType(); }
        }

        // a dictionary of <description, function name> of all functions used to set additional parameters
        public Dictionary<string, string> SetFunctionsForAddedParams
        {
            get { return s_SetFunctionsForAddedParams; }
        }

        public float PercentOfEnergyRemaining
        {
            get { return m_Engine.PercentOfEnergyRemaining; }
        }

        // ==================================================== overrides and operators ====================================================
        public override bool Equals(object obj)
        {
            bool equals = false;
            Vehicle compareTo = obj as Vehicle;

            if (compareTo != null)
            {
                equals = m_LicensePlate.Equals(compareTo.m_LicensePlate);
            }

            return equals;
        }

        public static bool operator ==(Vehicle i_Vehicle1, Vehicle i_Vehicle2)
        {
            bool equals = false;

            if (i_Vehicle1 == null || i_Vehicle2 == null)
            {
                equals = false;
            }
            else if (i_Vehicle1.Equals(i_Vehicle2))
            {
                equals = true;
            }

            return equals;
        }

        public static bool operator !=(Vehicle i_Vehicle1, Vehicle i_Vehicle2)
        {
            return !(i_Vehicle1 == i_Vehicle2);
        }

        public override int GetHashCode()
        {
            return m_LicensePlate.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format(
@"License plate: {0}
Model Name: {1}
Vehicle type: {2}

Wheels Information:
Manufacturer: {3}
Max air pressure: {4}

Engine Information:
{5}
",
LicensePlate,
ModelName,
this.GetType().Name,
m_Wheels[0].Manufacturer,
m_Wheels[0].MaxAirPressure,
m_Engine.ToString()
);
        }

        // ==================================================== Methods ====================================================
        // initialize all wheels of a car, this is done from the ctor of the inheritant once we know the actual numWheels in runtime
        protected void InitAllWheels(string i_WheelManufacturer, float i_MaxAirPress, byte i_NumWheels)
        {
            for (byte i = 0; i < i_NumWheels; i++)
            {
                m_Wheels.Add(new Wheel(i_WheelManufacturer, i_MaxAirPress));
            }
        }

        // fill all wheels in the vehicle to maximum air pressure
        public void FillAllWheelsToMaxAirPress()
        {
            foreach (Wheel wheel in m_Wheels)
            {
                float airToFill = wheel.MaxAirPressure - wheel.CurrentAirPressure;

                wheel.FillAir(airToFill);
            }
        }

        // charge an electric engine (if the caller does not have an electric engine an exception is thrown)
        public void Charge(float i_AmountEnergyToFill)
        {
            if (m_Engine.GetType() == typeof(ElectricEngine))
            {
                ((ElectricEngine)m_Engine).Charge(i_AmountEnergyToFill);
            }
            else
            {
                string exceptionMessage = string.Format("Can't charge an engine of type : {0} ", m_Engine.GetType());
                throw new ArgumentException(exceptionMessage);
            }
        }

        // fuel a fuelable engine (if the caller does not have an electric engine an exception is thrown)
        public void FillFuel(float i_AmountEnergyToFill, eFuelType i_FuelType)
        {
            if (m_Engine.GetType() == typeof(MotorEngine))
            {
                ((MotorEngine)m_Engine).FillFuel(i_AmountEnergyToFill, i_FuelType);
            }
            else
            {
                string exceptionMessage = string.Format("Can't fuel an engine of type : {0} ", m_Engine.GetType());
                throw new ArgumentException(exceptionMessage);
            }
        }
    }
}