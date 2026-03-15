using SplineLearn.Data;
using Unity.Burst;
using Unity.Mathematics;

namespace SplineLearn.Utilities
{
    public static class CenterExtensions
    {
        [BurstCompile]
        public static void Center(in this BoundsData boundsData, out float3 center)
        {
            ScaledSumExtensions.ScaledSum(boundsData.min, boundsData.max, NumbersRef.Half, out center);
        }
    }
}