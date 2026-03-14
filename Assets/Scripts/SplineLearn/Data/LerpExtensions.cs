using Unity.Burst;
using Unity.Mathematics;

namespace SplineLearn.Data
{
    public static class LerpExtensions
    {
        /// <summary>
        /// Linearly interpolates between two SplinePoints. 
        /// </summary>
        [BurstCompile]
        public static void Lerp(in this SplinePoint a, in SplinePoint b, in float t, out SplinePoint result)
        {
            var position = math.lerp(a.position, b.position, t);
            var tangent = math.lerp(a.tangent, b.tangent, t);
            var up = math.lerp(a.up, b.up, t);

            result = new SplinePoint
            {
                position = position,
                tangent = tangent,
                up = up
            };
        }
    }
}