using SplineLearn.Data;
using Unity.Burst;

namespace SplineLearn.Utilities
{
    /// <summary>
    /// Translates the human-readable Accuracy enum into a mathematical multiplier.
    /// </summary>
    public static class AccuracyExtensions
    {
        [BurstCompile]
        public static void ToSearchIntervalScalar(this Accuracy accuracy, out float scalar)
        {
            scalar = accuracy switch
            {
                Accuracy.BestPerformance => 6f,
                Accuracy.PreferPerformance => NumbersRef.Four,
                Accuracy.Balanced => 3f,
                Accuracy.PreferPrecision => NumbersRef.One,
                Accuracy.HighestPrecision => NumbersRef.Half,
                _ => NumbersRef.One
            };
        }
    }
}