namespace SplineLearn.Data
{
    /// <summary>
    /// Defines how the input mesh should be aligned relative to the spline curve.
    /// </summary>
    public enum Alignment : byte
    {
        PivotPoint = 0,
        TopLeft = 1,
        TopCenter = 2,
        TopRight = 3,
        MiddleLeft = 4,
        MiddleCenter = 5,
        MiddleRight = 6,
        BottomLeft = 7,
        BottomCenter = 8,
        BottomRight = 9
    }
}