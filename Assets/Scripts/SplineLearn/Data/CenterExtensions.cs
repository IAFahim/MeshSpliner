using Unity.Burst;
using Unity.Mathematics;

namespace SplineLearn.Data
{
    public static class CenterExtensions
    {
        [BurstCompile]
        public static void Center(in this BoundsData boundsData, out float3 center)
        {
            ScaledSumExtensions.ScaledSum(boundsData.min, boundsData.max, 0.5f, out center);
        }
    }
}