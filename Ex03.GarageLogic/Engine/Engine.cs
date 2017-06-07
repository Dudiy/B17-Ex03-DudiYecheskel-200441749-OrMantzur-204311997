﻿namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        protected float m_EnergyRemaining;
        protected float m_MaxEnergy;

        public Engine(float i_MaxEnergy)
        {
            m_MaxEnergy = i_MaxEnergy;
            m_EnergyRemaining = m_MaxEnergy;
        }

        public float PercentOfEnergyRemaining
        {
            get { return m_EnergyRemaining / m_MaxEnergy; }
        }

        public float MaxEnergy
        {
            get { return m_MaxEnergy; }
        }

        protected void FillEnergy(float i_EnergyToFill)
        {
            if (m_EnergyRemaining + i_EnergyToFill <= m_MaxEnergy)
            {
                m_EnergyRemaining += i_EnergyToFill;
            }
            else
            {
                // (m_MaxEnergy - m_EnergyRemaining) is the max value that can fill
                throw new ValueOutOfRangeException(0, m_MaxEnergy - m_EnergyRemaining);
            }
        }

        public abstract override string ToString();
    }
}
