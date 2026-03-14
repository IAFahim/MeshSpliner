using System;
using Unity.Mathematics;

namespace SplineLearn.Data
{
    /// <summary>
    /// Represents the physical boundaries of a mesh.
    /// </summary>
    [Serializable]
    public struct BoundsData
    {
        public float3 size;
        public float3 min;
        public float3 max;
    }
}