namespace SplineLearn.Data
{
    /// <summary>
    /// Dictates how dense our spline sampling should be.
    /// </summary>
    public enum Accuracy : byte
    {
        BestPerformance = 0,
        PreferPerformance = 1,
        Balanced = 2,
        PreferPrecision = 3,
        HighestPrecision = 4,
    }
}