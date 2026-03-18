using SplineLearn.Data;
using Unity.Burst;
using Unity.Mathematics;

namespace SplineLearn.Utilities
{
    public static class CenterExtensions
    {
        /// <summary>
        /// Calculates the exact center of a bounding box.
        /// </summary>
        [BurstCompile]
        public static void Center(in this BoundsData boundsData, out float3 center)
        {
            center = (boundsData.min + boundsData.max) * NumbersRef.Half;
        }
    }
}