namespace InferenceLibrary
{
    public static class MamdaniInference
    {
        /// <summary>
        /// Mamdani implication (MIN).
        /// </summary>
        public static IEnumerable<double[]> Implication(IEnumerable<(FuzzyTuple fuzzyTuple, Severity OutputSeverity)> rules)
        {
            var result = new List<double[]>();

            foreach (var rule in rules)
            {
                //Find min among input membership functions
                var inputMfsMin = Math.Min(rule.fuzzyTuple.ML.Value, Math.Min(rule.fuzzyTuple.CS1.Value, rule.fuzzyTuple.CS2.Value));

                //Find min among values of output membership function and input min
                var fuzzyMin = Model.OutputLV.TermValues[rule.OutputSeverity]
                    .Select(x => Math.Min(x, inputMfsMin));

                result.Add(fuzzyMin.ToArray());
            }

            return result;
        }

        /// <summary>
        /// Mamdani aggregation (MAX).
        /// </summary>
        public static double[] Aggregation(IEnumerable<double[]> fuzzySets)
        {
            var fuzzySetsArray = fuzzySets.ToArray();

            var result = new double[fuzzySetsArray[0].Length];

            //Find max values among arrays
            for (int i = 0; i < fuzzySetsArray[0].Length; i++)
            {
                result[i] = fuzzySetsArray[0][i];

                for (var j = 1; j < fuzzySetsArray.Length; j++)
                    if (result[i] < fuzzySetsArray[j][i])
                        result[i] = fuzzySetsArray[j][i];
            }

            return result;
        }

        /// <summary>
        /// Mamdani processing.
        /// </summary>
        public static (double crispResult, (Severity term, double value) wordResult) Process(double[] crispInput)
        {
            var fuzzyValues = Fuzzifier.Fuzzification(crispInput);

            var fuzzyTuples = GenerateFuzzyTuples(fuzzyValues);
            var rules = fuzzyTuples.Select(t => (t, Model.RuleProcessor.ActivateRule(t)));

            var fuzzySets = Implication(rules);
            var fuzzySet = Aggregation(fuzzySets);

            var crispResult = Defuzzifier.Defuzzification(fuzzySet);
            var wordResult = Defuzzifier.GetWordResult(fuzzySet);

            return (crispResult, wordResult);
        }

        /// <summary>
        /// Create all possible tuples of Fuzzy values.
        /// </summary>
        private static IEnumerable<FuzzyTuple> GenerateFuzzyTuples(Dictionary<Severity, double>[] fuzzyValues)
        {
            if (fuzzyValues.Length != 3)
                throw new ArgumentException(nameof(fuzzyValues));

            var values = new List<FuzzyTuple>();

            foreach (var ml in fuzzyValues[0])
                foreach (var cs1 in fuzzyValues[1])
                    foreach (var cs2 in fuzzyValues[2])
                        values.Add(new FuzzyTuple(ml, cs1, cs2));

            return values;
        }
    }
}
