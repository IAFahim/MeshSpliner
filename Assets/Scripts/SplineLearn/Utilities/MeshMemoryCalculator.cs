using UnityEngine;
using UnityEngine.Rendering;

namespace SplineLearn.Utilities
{
    /// <summary>
    /// Calculates how much RAM a mesh consumes. 
    /// This is NOT a Burst-compiled job, as it interacts directly with the UnityEngine.Mesh class.
    /// </summary>
    public static class MeshMemoryCalculator
    {
        /// <summary>
        /// Calculates the size of a mesh in Megabytes.
        /// </summary>
        public static float GetMemorySizeMB(Mesh mesh)
        {
            if (mesh == null) return NumbersRef.Zero;

            float sizeInBytes = NumbersRef.Zero;
            int vertexCount = mesh.vertexCount;

            for (int stream = 0; stream < mesh.vertexBufferCount; stream++)
            {
                int stride = mesh.GetVertexBufferStride(stream);
                sizeInBytes += vertexCount * stride;
            }

            long indexCount = 0;
            for (int submesh = 0; submesh < mesh.subMeshCount; submesh++)
            {
                indexCount += mesh.GetIndexCount(submesh);
            }

            bool use32Bit = mesh.indexFormat == IndexFormat.UInt32;
            sizeInBytes += indexCount * (use32Bit ? sizeof(uint) : sizeof(ushort));
            
            return sizeInBytes / (NumbersRef.KB * NumbersRef.KB);
        }
    }
}