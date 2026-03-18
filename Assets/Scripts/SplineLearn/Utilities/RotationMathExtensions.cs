using Unity.Burst;
using Unity.Mathematics;

namespace SplineLearn.Utilities
{
    public class RotationMathExtensions
    {
        /// <summary>
        /// Locks specific rotation axes of a target rotation, replacing them with the neutral rotation's axes.
        /// </summary>
        [BurstCompile]
        public static quaternion LockRotationAngle(
            quaternion neutralRotation, quaternion targetRotation,
            bool3 lockAngles
        )
        {
            math.RotationOrder rotationOrder = math.RotationOrder.ZXY;

            float3 prevEuler = math.Euler(neutralRotation, rotationOrder);
            float3 newEuler = math.Euler(targetRotation, rotationOrder);

            if (lockAngles.x) newEuler.x = prevEuler.x;
            if (lockAngles.y) newEuler.y = prevEuler.y;
            if (lockAngles.z) newEuler.z = prevEuler.z;

            return quaternion.Euler(newEuler, rotationOrder);
        }
    }
}