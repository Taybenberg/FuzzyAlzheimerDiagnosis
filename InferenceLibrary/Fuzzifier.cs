namespace InferenceLibrary
{
    public static class Fuzzifier
    {
        /// <summary>
        /// Transform a crisp set into a fuzzy set.
        /// </summary>
        public static Dictionary<Severity, double>[] Fuzzification(double[] crispValues)
        {
            if (Model.InputLVs.Length != crispValues.Length)
                throw new ArgumentException(nameof(crispValues));

            var results = new Dictionary<Severity, double>[crispValues.Length];

            for (int i = 0; i < crispValues.Length; i++)
            {
                results[i] = new();

                foreach (var term in Model.InputLVs[i].Terms)
                {
                    //Get index of nearest double value
                    var index = Model.InputLVs[i].XValues
                        .Select((x, i) => (x, i))
                        .OrderBy(t => Math.Abs(t.x - crispValues[i]))
                        .First().i;

                    //Get value of linguistic term
                    var value = Model.InputLVs[i].TermValues[term.Key][index];

                    if (value > 0)
                        results[i].Add(term.Key, value);
                }
            }

            return results;
        }
    }
}
