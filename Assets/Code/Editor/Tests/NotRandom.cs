using System.Collections.Generic;
using com.enemyhideout.loot;

namespace com.enemyhideout.loot.tests
{
    /// <summary>
    /// A class that implements IRandom that is actually not random, so we have predictable outcomes for purposes of testing.
    /// </summary>
    public class NotRandom : IRandom
    {

        private List<float> _values;
        private int index;

        public NotRandom(float value)
        {
            _values = new List<float>() {value};
        }
        
        public float Range(float minInclusive, float maxInclusive)
        {
            var retVal = _values[index % _values.Count];
            index++;
            return minInclusive + retVal;
        }
    }
}