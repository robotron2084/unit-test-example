namespace com.enemyhideout.loot
{
    /// <summary>
    /// An interface into randomness that we can pass to items that use randomness.
    /// </summary>
    public interface IRandom
    {
        public float Range(float minInclusive, float maxInclusive);
        
    }
}