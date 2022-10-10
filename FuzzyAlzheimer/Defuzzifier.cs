namespace FuzzyAlzheimer
{
    public static class Defuzzifier
    {
        public static (Severity term, double value) GetWordResult(double[] fuzzySet)
        {
            var words = new List<(Severity, double)>();

            foreach (var term in Model.OutputLV.Terms)
            {
                var result = JaccardMeasure(Model.OutputLV.Values[term.Key], fuzzySet);
                words.Add((term.Key, result));
            }

            return words.MaxBy(x => x.Item2);
        }

        private static double JaccardMeasure(double[] mf, double[] fuzzySet)
        {
            var min = mf.Zip(fuzzySet, (a, b) => Math.Min(a, b));
            var max = mf.Zip(fuzzySet, (a, b) => Math.Max(a, b));

            return min.Sum() / max.Sum();
        }

        public static double Defuzzification(double[] fuzzySet)
        {
            return Cog(fuzzySet);
        }

        private static double Cog(double[] fuzzyValues)
        {
            var merged = Model.OutputLV.X.Zip(fuzzyValues, (a, b) => a * b);
            return merged.Sum() / fuzzyValues.Sum();
        }
    }
}
