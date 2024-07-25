using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MaxValue;
        private readonly float r_MinValue;

        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue)
        {
            r_MaxValue = i_MaxValue;
            r_MinValue = i_MinValue;
        }

        public override string Message
        {
            get
            {
                return string.Format(@"Value is out of range. Valid range: {0} - {1}.", r_MinValue, r_MaxValue);
            }
        }
    }

}
