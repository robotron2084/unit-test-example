using com.enemyhideout.loot;

namespace com.enemyhideout.utils
{
    /// <summary>
    /// Wraps Unity's randomness implementation and exposes it as an IRandom interface.
    /// </summary>
    public class EnemyRandom : IRandom
    {
        public float Range(float minInclusive, float maxInclusive)
        {
            return UnityEngine.Random.Range(minInclusive, maxInclusive);
        }
    }
}