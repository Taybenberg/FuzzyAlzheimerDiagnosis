namespace FuzzyAlzheimer
{
    public class Program
    {
        public static void Preprocessing()
        {
            Model.OutputLV.Values = new();
            foreach (var term in Model.OutputLV.Terms)
            {
                //Calculate membership function value for each x 
                var termValues = Model.OutputLV.X
                    .Select(x => term.Value.Invoke(x))
                    .ToArray();

                Model.OutputLV.Values.Add(term.Key, termValues);
            }

            foreach (var inputLV in Model.InputLVs)
            {
                inputLV.Values = new();
                foreach (var term in inputLV.Terms)
                {
                    var termValues = inputLV.X
                        .Select(x => term.Value.Invoke(x))
                        .ToArray();

                    inputLV.Values.Add(term.Key, termValues);
                }
            }
        }

        public static IEnumerable<FuzzyTuple> GenerateFuzzyTuples(Dictionary<Severity, double>[] fuzzyValues)
        {
            if (fuzzyValues.Length != 3)
                throw new ArgumentException(nameof(fuzzyValues));

            var values = new List<FuzzyTuple>();

            //Create all possible tuples of fuzzy values
            foreach (var ml in fuzzyValues[0])
                foreach (var cs1 in fuzzyValues[1])
                    foreach (var cs2 in fuzzyValues[2])
                        values.Add(new FuzzyTuple(ml, cs1, cs2));

            return values;
        }

        public static void Main()
        {
            Preprocessing();

            var crispInput = new double[] { 4.0, 3.0, 7.0 };

            var fuzzyValues = Fuzzifier.Fuzzification(crispInput);

            var fuzzyTuples = GenerateFuzzyTuples(fuzzyValues);
            var rules = fuzzyTuples.Select(t => (t, Model.ActivateRule(t)));

            var fuzzySets = MamdaniInference.Implication(rules);
            var fuzzySet = MamdaniInference.Aggregation(fuzzySets);

            var crispResult = Defuzzifier.Defuzzification(fuzzySet);
            var wordResult = Defuzzifier.GetWordResult(fuzzySet);

            Console.WriteLine($"{wordResult} with score {crispResult}");
        }
    }
}
