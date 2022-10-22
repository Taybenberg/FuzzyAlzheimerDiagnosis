namespace InferenceLibrary
{
    public static class Defuzzifier
    {
        /// <summary>
        /// Obtain a single term from the output of the aggregated fuzzy set.
        /// </summary>
        public static (Severity term, double value) GetWordResult(double[] fuzzySet)
        {
            var words = new List<(Severity, double)>();

            foreach (var term in Model.OutputLV.Terms)
            {
                var result = JaccardMeasure(Model.OutputLV.TermValues[term.Key], fuzzySet);
                words.Add((term.Key, result));
            }

            return words.MaxBy(x => x.Item2);
        }

        /// <summary>
        /// Measure of similarity for the two sets of data.
        /// </summary>
        private static double JaccardMeasure(double[] mf, double[] fuzzySet)
        {
            var min = mf.Zip(fuzzySet, (a, b) => Math.Min(a, b));
            var max = mf.Zip(fuzzySet, (a, b) => Math.Max(a, b));

            return min.Sum() / max.Sum();
        }

        /// <summary>
        /// Obtain a single number from the output of the aggregated fuzzy set.
        /// </summary>
        public static double Defuzzification(double[] fuzzySet)
        {
            return Cog(fuzzySet);
        }

        /// <summary>
        /// This method provides a crisp value based on the center of gravity of the fuzzy set.
        /// </summary>
        private static double Cog(double[] fuzzyValues)
        {
            var merged = Model.OutputLV.XValues
                .Zip(fuzzyValues, (a, b) => a * b);

            return merged.Sum() / fuzzyValues.Sum();
        }
    }
}
