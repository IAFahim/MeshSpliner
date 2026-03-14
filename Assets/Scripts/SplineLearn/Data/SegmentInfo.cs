using System;

namespace SplineLearn.Data
{
    /// <summary>
    /// Holds segmentation data for repeating a mesh.
    /// </summary>
    [Serializable]
    public class SegmentInfo
    {
        public int tileCount;
        public float tileLength;
        public float meshLength;
    }
}