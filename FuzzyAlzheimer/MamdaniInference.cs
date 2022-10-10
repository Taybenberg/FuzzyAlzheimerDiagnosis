namespace FuzzyAlzheimer
{
    public static class MamdaniInference
    {
        public static IEnumerable<double[]> Implication(IEnumerable<(FuzzyTuple fuzzyTuple, Severity OutputSeverity)> rules)
        {
            var result = new List<double[]>();

            foreach (var rule in rules)
            {
                //Find min among input membership functions
                var inputMfsMin = Math.Min(rule.fuzzyTuple.ML.Value, Math.Min(rule.fuzzyTuple.CS1.Value, rule.fuzzyTuple.CS2.Value));

                //Find min among values of output membership function and input min
                var fuzzyMin = Model.OutputLV.Values[rule.OutputSeverity]
                    .Select(x => Math.Min(x, inputMfsMin));

                result.Add(fuzzyMin.ToArray());
            }

            return result;
        }

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
    }
}
