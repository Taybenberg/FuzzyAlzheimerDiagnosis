using static FuzzyAlzheimer.Severity;

namespace FuzzyAlzheimer
{
    public static class Model
    {
        /// <summary>
        /// Input Linguistic variables.
        /// </summary>
        public static readonly LinguisticVariable[] InputLVs = new[]
        {
            new LinguisticVariable
            {
                //C# generates only int range
                //Generate int from 0 to 1000, then scale to double 0-10
                X = Enumerable.Range(0, (int)(10 / 0.01)).Select(i => i * 0.01).ToArray(), //0-10, step 0.01
                Name = "Memory Loss",
                Terms = new()
                {
                    [Normal] = (double x) => MembershipFunctions.Sigmf(x, -11.9, 2.5),
                    [Mild] = (double x) => MembershipFunctions.Gauss2mf(x, 0.506, 3.105, 0.506, 4.42),
                    [Moderate] = (double x) => MembershipFunctions.Gauss2mf(x, 0.48, 5.59, 0.48, 6.971),
                    [Severe] = (double x) => MembershipFunctions.Sigmf(x, 10.5, 7.54),
                }
            },

            new LinguisticVariable
            {
                X = Enumerable.Range(0, (int)(10 / 0.01)).Select(i => i * 0.01).ToArray(), //0-10, step 0.01
                Name = "Cognitive Symptom 1",
                Terms = new()
                {
                    [Normal] = (double x) => MembershipFunctions.Sigmf(x, -11.9, 2.5),
                    [Mild] = (double x) => MembershipFunctions.Gauss2mf(x, 0.506, 3.105, 0.506, 4.42),
                    [Moderate] = (double x) => MembershipFunctions.Gauss2mf(x, 0.48, 5.59, 0.48, 6.971),
                    [Severe] = (double x) => MembershipFunctions.Sigmf(x, 10.5, 7.54),
                }
            },

            new LinguisticVariable
            {
                X = Enumerable.Range(0, (int)(10 / 0.01)).Select(i => i * 0.01).ToArray(), //0-10, step 0.01
                Name = "Cognitive Symptom 2",
                Terms = new()
                {
                    [Normal] = (double x) => MembershipFunctions.Sigmf(x, -11.9, 2.5),
                    [Mild] = (double x) => MembershipFunctions.Gauss2mf(x, 0.506, 3.105, 0.506, 4.42),
                    [Moderate] = (double x) => MembershipFunctions.Gauss2mf(x, 0.48, 5.59, 0.48, 6.971),
                    [Severe] = (double x) => MembershipFunctions.Sigmf(x, 10.5, 7.54),
                }
            },
        };

        /// <summary>
        /// Output Linguistic variable.
        /// </summary>
        public static readonly LinguisticVariable OutputLV = new()
        {
            X = Enumerable.Range(0, (int)(10 / 0.01)).Select(i => i * 0.01).ToArray(), //0-10, step 0.01
            Name = "ADIN",
            Terms = new()
            {
                [Normal] = (double x) => MembershipFunctions.Sigmf(x, -11.9, 1.49),
                [Mild] = (double x) => MembershipFunctions.Gauss2mf(x, 0.506, 2.1, 0.506, 4.011),
                [Moderate] = (double x) => MembershipFunctions.Gauss2mf(x, 0.48, 5.17, 0.48, 6.839),
                [Severe] = (double x) => MembershipFunctions.Sigmf(x, 10.5, 7.377),
            }
        };

        /// <summary>
        /// Model Rule base.
        /// </summary>
        /// <returns>ADIN Severity</returns>
        public static Severity ActivateRule(FuzzyTuple fuzzyTuple) =>
            (fuzzyTuple.ML.Key, fuzzyTuple.CS1.Key, fuzzyTuple.CS2.Key) switch
            {   
                //If ML is %% and CS is %% and CS2 is %% then ADIN is %%
                (Normal, Normal, Normal) => Normal,     //1
                (Normal, Normal, Mild) => Normal,       //2
                (Normal, Normal, Moderate) => Normal,   //3

                //...

                (Mild, Severe, Moderate) => Moderate,   //31
                (Mild, Severe, Severe) => Moderate,     //32
                (Moderate, Mild, Mild) => Mild,         //33

                //...

                (Severe, Severe, Mild) => Severe,       //62
                (Severe, Severe, Moderate) => Severe,   //63
                (Severe, Severe, Severe) => Severe,     //64

                _ => Severe
            };
    }
}
