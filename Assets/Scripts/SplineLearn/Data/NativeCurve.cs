using System;
using Unity.Burst;
using Unity.Collections;

namespace SplineLearn.Data
{
    /// <summary>
    /// A Burst-compatible version of Unity's AnimationCurve.
    /// It samples the managed curve into an unmanaged NativeArray.
    /// </summary>
    [BurstCompile]
    public struct NativeCurve : IDisposable
    {
        [ReadOnly] private NativeArray<float> _samples;
        private const int SampleCount = 16;

        public void Dispose()
        {
            if (_samples.IsCreated)
            {
                _samples.Dispose();
            }
        }
    }
}