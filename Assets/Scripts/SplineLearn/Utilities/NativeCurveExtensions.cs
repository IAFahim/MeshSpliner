using SplineLearn.Data;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

namespace SplineLearn.Utilities
{
    public static class NativeCurveExtensions
    {
        /// <summary>
        /// Allocates a NativeArray and samples an AnimationCurve into it.
        /// </summary>
        [BurstCompile]
        public static void CreateAndSampleCurve(
            this ref NativeArray<float> samples,
            in AnimationCurve sourceCurve,
            Allocator allocator,
            in int sampleCount
        )
        {
            samples = new NativeArray<float>(sampleCount, allocator, NativeArrayOptions.UninitializedMemory);
            samples.SampleAnimationCurve(sourceCurve);
        }

        /// <summary>
        /// Samples a managed AnimationCurve into the NativeArray.
        /// This method cannot run inside Burst.
        /// </summary>
        [BurstCompile]
        public static void SampleAnimationCurve(
            this NativeArray<float> samples,
            in AnimationCurve sourceCurve
        )
        {
            int sampleCount = samples.Length;

            unsafe
            {
                float* samplePtr = (float*)samples.GetUnsafePtr();
                float maxSampleIndex = sampleCount - NumbersRef.One;

                for (int i = 0; i < sampleCount; i++)
                {
                    float t = i / maxSampleIndex;
                    samplePtr[i] = sourceCurve.Evaluate(t);
                }
            }
        }

        /// <summary>
        /// Evaluates the sampled curve using linear interpolation.
        /// Safe for Burst jobs.
        /// </summary>
        [BurstCompile]
        public static void EvaluateSampledCurve(
            this in NativeArray<float> samples,
            float normalizedTime,
            out float normalizedSample
        )
        {
            int sampleCount = samples.Length;
            normalizedTime = math.clamp(normalizedTime, 0f, 1f);

            var lastSample = sampleCount - 1;
            float scaled = normalizedTime * lastSample;

            int index = (int)math.floor(scaled);

            var nextCurrent = index + 1;
            int nextIndex = math.min(nextCurrent, lastSample);

            float fraction = scaled - index;
            normalizedSample = math.lerp(samples[index], samples[nextIndex], fraction);
        }
    }
}