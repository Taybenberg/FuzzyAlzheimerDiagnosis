namespace FuzzyAlzheimer
{
    public class LinguisticVariable
    {
        /// <summary>
        /// X axis range.
        /// </summary>
        public double[] X { get; set; }

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
        public Dictionary<Severity, double[]> Values { get; set; }
    }
}
