using Unity.Burst;

namespace SplineLearn.Utilities
{
    /// <summary>
    /// Provides simple mathematical easing functions.
    /// </summary>
    public static class EasingExtensions
    {
        /// <summary>
        /// Smoothly accelerates from 0, then decelerates to 1.
        /// </summary>
        [BurstCompile]
        public static float EaseInOut(this float t)
        {
            float eased = NumbersRef.Two * t * t;
            if (t > NumbersRef.Half) eased = NumbersRef.Four * t - eased - NumbersRef.One;
            return eased;
        }
    }
}