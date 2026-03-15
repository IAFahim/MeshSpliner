using Unity.Burst;
using Unity.Mathematics;

namespace SplineLearn.Utilities
{
    public static class ScaledSumExtensions
    {
        [BurstCompile]
        public static void ScaledSum(in float3 a, in float3 b, float ratio, out float3 result)
        {
            result = (a + b) * ratio;
        }
    }
}