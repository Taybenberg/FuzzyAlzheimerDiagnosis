namespace InferenceLibrary
{
    public static class Fuzzifier
    {
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
                    var index = Model.InputLVs[i].X
                        .Select((x, i) => (x, i))
                        .OrderBy(t => Math.Abs(t.x - crispValues[i]))
                        .First().i;

                    //nearest result of term membership function
                    var value = Model.InputLVs[i].Values[term.Key][index];

                    if (value > 0)
                        results[i].Add(term.Key, value);
                }
            }

            return results;
        }
    }
}
