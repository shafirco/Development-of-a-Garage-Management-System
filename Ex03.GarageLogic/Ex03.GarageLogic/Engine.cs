using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        protected float m_CurrentEnergyQuantity;
        protected readonly float r_MaximumEnergyAmount;
        protected float m_EnergyLevelPercentage;

        internal virtual void PrintFullInfo()
        {
            Console.WriteLine("Engine info:");
            Console.WriteLine($"Current energy quantity: {m_CurrentEnergyQuantity}");
            Console.WriteLine($"Maximum energy quantity: {r_MaximumEnergyAmount}");
            Console.WriteLine($"Energy level: {m_EnergyLevelPercentage:F2}%");
        }

        public Engine(float i_MaxEnergyAmount)
        {
            r_MaximumEnergyAmount = i_MaxEnergyAmount;
        }

        public float MaximumEnergyAmount
        {
            get { return r_MaximumEnergyAmount; }
        }

        public float CurrentEnergyQuantity
        {
            get { return m_CurrentEnergyQuantity; }
            set
            {
                if (value >= 0 && value <= r_MaximumEnergyAmount)
                {
                    m_CurrentEnergyQuantity = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(r_MaximumEnergyAmount, 0);
                }
            }
        }

        public void EnergyFilling(float AmountEnergyToFill)
        {
            if (AmountEnergyToFill < 0 || (AmountEnergyToFill + m_CurrentEnergyQuantity > r_MaximumEnergyAmount))
            {
                throw new ValueOutOfRangeException(r_MaximumEnergyAmount - m_CurrentEnergyQuantity, 0);
            }
            else
            {
                m_CurrentEnergyQuantity += AmountEnergyToFill;
                m_EnergyLevelPercentage = m_CurrentEnergyQuantity / r_MaximumEnergyAmount * 100;
            }
        }

        public void setUniqueEngineFields(string i_EnergyAmount)
        {
            float eneregy;
            if (!float.TryParse(i_EnergyAmount, out eneregy))
            {
                throw new FormatException("Incorrect energy amaount.");
            }
            if (eneregy > r_MaximumEnergyAmount || eneregy < 0)
            {
                throw new ValueOutOfRangeException(r_MaximumEnergyAmount, 0);
            }
            EnergyFilling(eneregy);
            
        }

        public abstract void GetEngineFieldsNames(List<string> i_FieldsNames);
    }
}
