using NUnit.Framework;
using SplineLearn.Data;
using SplineLearn.Utilities;

namespace SplineLearn.Tests.Utilities
{
    public class AccuracyExtensionsTests
    {
        [TestCase(Accuracy.BestPerformance, 6f)]
        [TestCase(Accuracy.PreferPerformance, NumbersRef.Four)]
        [TestCase(Accuracy.Balanced, 3f)]
        [TestCase(Accuracy.PreferPrecision, NumbersRef.One)]
        [TestCase(Accuracy.HighestPrecision, NumbersRef.Half)]
        public void ToSearchIntervalScalar_ReturnsCorrectValue(Accuracy accuracy, float expected)
        {
            // Act
            accuracy.ToSearchIntervalScalar(out float result);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ToSearchIntervalScalar_InvalidCast_ReturnsFallbackOne()
        {
            // Arrange
            var invalidAccuracy = (Accuracy)99;

            // Act
            invalidAccuracy.ToSearchIntervalScalar(out var scalar);

            // Assert
            Assert.AreEqual(NumbersRef.One, scalar, "An undefined Accuracy enum should fallback to 1f");
        }
    }
}