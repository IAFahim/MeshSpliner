using NUnit.Framework;
using SplineLearn.Utilities;
using Unity.Mathematics;

namespace SplineLearn.Tests.Utilities
{
    public class ScaledSumExtensionsTests
    {
        [TestCase(
            0, 0, 0,
            0, 0, 0,
            1f,
            0, 0, 0
        )]
        [TestCase(
            0, 0, 0,
            1, 1, 1,
            1f,
            1, 1, 1
        )]
        [TestCase(
            0, 0, 0,
            1, 1, 1,
            .5f,
            .5f, .5f, .5f
        )]
        
        [TestCase(
            -1, -1, -1,
            1, 1, 1,
            .5f,
            0, 0, 0
        )]
        public void ScaledSum_CalculatesCorrectly(
            float ax, float ay, float az,
            float bx, float by, float bz,
            float ratio,
            float ex, float ey, float ez
        )
        {
            float3 a = new float3(ax, ay, az);
            float3 b = new float3(bx, by, bz);

            ScaledSumExtensions.ScaledSum(a, b, ratio, out var result);

            Assert.AreEqual(ex, result.x, math.EPSILON);
            Assert.AreEqual(ey, result.y, math.EPSILON);
            Assert.AreEqual(ez, result.z, math.EPSILON);
        }
    }
}