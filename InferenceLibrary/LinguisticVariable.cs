namespace InferenceLibrary
{
    public class LinguisticVariable
    {
        /// <summary>
        /// X axis range.
        /// </summary>
        public XRange X { get; set; }

        /// <summary>
        /// X axis values.
        /// </summary>
        public double[] XValues { get; set; }

        /// <summary>
        /// Linguistic variable name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Dictionary of Linquistic terms.
        /// </summary>
        public Dictionary<Severity, Func<double, double>> Terms { get; set; }

        /// <summary>
        /// Dictionary of results of Linguistic terms.
        /// </summary>
        public Dictionary<Severity, double[]> TermValues { get; set; }
    }
}
