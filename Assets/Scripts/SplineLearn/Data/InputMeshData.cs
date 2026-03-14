using System;
using Unity.Collections;
using Unity.Mathematics;

namespace SplineLearn.Data
{
    /// <summary>
    /// Extracts data from Unity's Mesh class so the Burst compiler can process it.
    /// </summary>
    public struct InputMeshData : IDisposable
    {
        [ReadOnly] public NativeArray<float3> Positions;
        [ReadOnly] public NativeArray<float3> Normals;
        [ReadOnly] public NativeArray<float4> Tangents;
        [ReadOnly] public NativeArray<float2> UV;

        /// <summary>
        /// Colors may be modified/overridden later by gradient jobs, so it lacks
        /// </summary>
        [ReadOnly] public NativeArray<float4> Colors;
        
        /// <summary>
        /// Triangles may be modified (e.g., reversed if we scale the mesh negatively)
        /// </summary>
        public NativeArray<ushort> SourceTriangles;

        /// <summary>
        /// Holds X: Start Index, Y: Index Count
        /// </summary>
        [ReadOnly] public NativeArray<int2> SourceSubmeshRanges;

        public ushort VertexCount;
        public int SubmeshCount;
        public BoundsData Bounds;

        public bool IsCreated => Positions.IsCreated;


        public void Dispose()
        {
            if (!IsCreated) return;
            
            Positions.Dispose();
            Normals.Dispose();
            Tangents.Dispose();
            UV.Dispose();
            Colors.Dispose();
            SourceTriangles.Dispose();
            SourceSubmeshRanges.Dispose();
        }
    }
}