namespace FuzzyAlzheimer
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
                    //nearest result of term membership function
                    var value = Model.InputLVs[i].Values[term.Key]
                        .OrderBy(x => Math.Abs(x - crispValues[i]))
                        .First();

                    if (value > 0)
                        results[i].Add(term.Key, value);
                }
            }

            return results;
        }
    }
}
