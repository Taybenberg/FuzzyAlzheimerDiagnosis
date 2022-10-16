namespace ModelCache
{
    /// <summary>
    /// Linguistic variable cached values.
    /// </summary>
    public record LvCache
    {
        public double[] XValues { get; set; }
        public Dictionary<int, double[]> TermValues { get; set; }
    }
}