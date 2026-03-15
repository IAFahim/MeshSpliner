using Unity.Burst;
using Unity.Mathematics;

namespace SplineLearn.Utilities
{
    public class DistanceWeightingExtensions
    {
        [BurstCompile]
        public static float CalculateWeight(
            float position, float surfaceLength, float startDistance,
            float startFalloff, float endDistance, float endFalloff,
            bool invert
        )
        {
            float startSafeFalloff = math.max(startFalloff, SplineConstants.Min);
            float endSafeFalloff = math.max(endFalloff, SplineConstants.Min);

            var start = math.saturate((startDistance - (position - (startDistance + startFalloff))) / startSafeFalloff);
            float end = math.saturate(((surfaceLength - endDistance) - (position + endDistance)) / endSafeFalloff);
            float gradient = math.max(start, NumbersRef.One - end);
            return invert ? NumbersRef.One - gradient : gradient;
        }

        [BurstCompile]
        public static float EdgeMask(
            float position, float maxWidth, float distance, float falloff,
            bool invert = false
        )
        {
            float safeFalloff = math.max(falloff, SplineConstants.Min);

            float start = math.saturate(((distance + safeFalloff) - (position - distance)) / safeFalloff);
            float end = math.saturate(((maxWidth - distance) - (position + distance)) / safeFalloff);
            float gradient = math.max(start, NumbersRef.One - end);
            return invert ? NumbersRef.One - gradient : gradient;
        }
    }
}