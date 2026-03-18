using SplineLearn.Data;
using SplineLearn.Utilities;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine.Splines;

namespace SplineLearn.Jobs
{
    /// <summary>
    /// The "Scout". Runs on multiple CPU cores to evaluate the heavy spline math once, 
    /// caching the results into a fast NativeArray.
    /// </summary>
    [BurstCompile]
    public struct SampleSplineJob : IJobParallelFor
    {
        [ReadOnly] public NativeSpline Spline;
        [ReadOnly] public bool Closed;
        [ReadOnly] public int SampleCount;

        [WriteOnly] public NativeArray<SplinePoint> Points;

        public void Execute(int i)
        {
            float t = i / (float)(SampleCount - 1);

            // Loop back to 0 if this is a closed circle and we are at the very end
            if (Closed && i == SampleCount) t = NumbersRef.Zero;

            Spline.Evaluate(t, out float3 position, out float3 tangent, out float3 up);

            // At the very start of a spline, tangents can occasionally calculate as zero. 
            // We sample slightly ahead to ensure we get a valid forward direction.
            if (i == 0)
            {
                Spline.Evaluate(SplineConstants.Min, out _, out tangent, out up);
            }

            // Same protection for the very end of the spline
            if (i == SampleCount)
            {
                Spline.Evaluate(NumbersRef.One - SplineConstants.Min, out _, out tangent, out up);
            }

            Points[i] = new SplinePoint
            {
                position = position,
                tangent = tangent,
                up = up
            };
        }
    }
}