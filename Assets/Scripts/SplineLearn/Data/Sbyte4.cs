using System;

namespace SplineLearn.Data
{
    /// <summary>
    /// A helper struct to pack 4 signed bytes tightly in memory.
    /// Used later to compress normal/tangent data.
    /// </summary>
    [Serializable]
    public struct Sbyte4
    {
        public sbyte x, y, z, w;
    }
}