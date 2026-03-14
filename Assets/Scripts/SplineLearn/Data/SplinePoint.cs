using System;
using Unity.Mathematics;

namespace SplineLearn.Data
{
    /// <summary>
    /// Represents a single evaluated point on a spline. 
    /// Unmanaged struct suitable for the Burst compiler.
    /// </summary>
    [Serializable]
    public struct SplinePoint
    {
        public float3 position;
        public float3 tangent;
        public float3 up;
    }
}
