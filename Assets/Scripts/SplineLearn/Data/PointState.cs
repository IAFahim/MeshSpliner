namespace SplineLearn.Data
{
    /// <summary>
    /// Identifies where a generated point lies relative to the spline curve.
    /// </summary>
    public enum PointState : byte
    {
        Inside,
        Edge,
        Outside
    }
}