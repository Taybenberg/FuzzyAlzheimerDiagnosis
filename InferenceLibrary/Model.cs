using static InferenceLibrary.Severity;

namespace InferenceLibrary
{
    public static class Model
    {
        /// <summary>
        /// Initialize model values by calculating Lingustic Terms. 
        /// </summary>
        public static void Initialize()
        {
            OutputLV.XValues = OutputLV.X.Range;
            OutputLV.TermValues = new();
            foreach (var term in OutputLV.Terms)
            {
                //Calculate membership function value for each x 
                var termValues = OutputLV.XValues
                    .Select(x => term.Value.Invoke(x))
                    .ToArray();

                OutputLV.TermValues.Add(term.Key, termValues);
            }

            foreach (var inputLV in InputLVs)
            {
                inputLV.XValues = inputLV.X.Range;
                inputLV.TermValues = new();
                foreach (var term in inputLV.Terms)
                {
                    var termValues = inputLV.XValues
                        .Select(x => term.Value.Invoke(x))
                        .ToArray();

                    inputLV.TermValues.Add(term.Key, termValues);
                }
            }
        }

        /// <summary>
        /// Input Linguistic variables.
        /// </summary>
        public static readonly LinguisticVariable[] InputLVs = new[]
        {
            new LinguisticVariable
            {
                X = new XRange(0, 10, 0.01), //0-10, step 0.01
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
                X = new XRange(0, 10, 0.01), //0-10, step 0.01
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
                X = new XRange(0, 10, 0.01), //0-10, step 0.01
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
            X = new XRange(0, 10, 0.01), //0-10, step 0.01
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
                (Normal, Normal, Normal) => Normal,         //1
                (Normal, Normal, Mild) => Normal,           //2
                (Normal, Normal, Moderate) => Normal,       //3
                (Normal, Normal, Severe) => Normal,         //4
                (Normal, Mild, Normal) => Normal,           //5
                (Normal, Moderate, Normal) => Normal,       //6
                (Normal, Severe, Normal) => Normal,         //7
                (Normal, Mild, Mild) => Normal,             //8
                (Normal, Mild, Moderate) => Normal,         //9
                (Normal, Mild, Severe) => Normal,           //10
                (Normal, Moderate, Mild) => Normal,         //11
                (Normal, Severe, Mild) => Normal,           //12
                (Normal, Moderate, Moderate) => Normal,     //13
                (Normal, Moderate, Severe) => Mild,         //14
                (Normal, Severe, Moderate) => Mild,         //15
                (Normal, Severe, Severe) => Mild,           //16
                (Mild, Normal, Normal) => Normal,           //17
                (Mild, Normal, Mild) => Normal,             //18
                (Mild, Normal, Moderate) => Mild,           //19
                (Mild, Normal, Severe) => Mild,             //20
                (Mild, Mild, Normal) => Normal,             //21
                (Mild, Moderate, Normal) => Mild,           //22
                (Mild, Severe, Normal) => Mild,             //23
                (Mild, Mild, Mild) => Mild,                 //24
                (Mild, Mild, Moderate) => Mild,             //25
                (Mild, Mild, Severe) => Moderate,           //26
                (Mild, Moderate, Mild) => Mild,             //27
                (Mild, Severe, Mild) => Moderate,           //28
                (Mild, Moderate, Moderate) => Moderate,     //29
                (Mild, Moderate, Severe) => Moderate,       //30
                (Mild, Severe, Moderate) => Moderate,       //31
                (Mild, Severe, Severe) => Moderate,         //32
                (Moderate, Normal, Normal) => Mild,         //33
                (Moderate, Normal, Mild) => Mild,           //34
                (Moderate, Normal, Moderate) => Moderate,   //35
                (Moderate, Normal, Severe) => Moderate,     //36
                (Moderate, Mild, Normal) => Mild,           //37
                (Moderate, Moderate, Normal) => Mild,       //38
                (Moderate, Severe, Normal) => Moderate,     //39
                (Moderate, Mild, Mild) => Mild,             //40
                (Moderate, Mild, Moderate) => Moderate,     //41
                (Moderate, Mild, Severe) => Severe,         //42
                (Moderate, Moderate, Mild) => Moderate,     //43
                (Moderate, Severe, Mild) => Severe,         //44
                (Moderate, Moderate, Moderate) => Moderate, //45
                (Moderate, Moderate, Severe) => Severe,     //46
                (Moderate, Severe, Moderate) => Severe,     //47
                (Moderate, Severe, Severe) => Severe,       //48
                (Severe, Normal, Normal) => Moderate,       //49
                (Severe, Normal, Mild) => Moderate,         //50
                (Severe, Normal, Moderate) => Severe,       //51
                (Severe, Normal, Severe) => Severe,         //52
                (Severe, Mild, Normal) => Moderate,         //53
                (Severe, Moderate, Normal) => Severe,       //54
                (Severe, Severe, Normal) => Severe,         //55
                (Severe, Mild, Mild) => Moderate,           //56
                (Severe, Mild, Moderate) => Severe,         //57
                (Severe, Mild, Severe) => Severe,           //58
                (Severe, Moderate, Mild) => Severe,         //59
                (Severe, Severe, Mild) => Severe,           //60
                (Severe, Moderate, Moderate) => Severe,     //61
                (Severe, Moderate, Severe) => Severe,       //62
                (Severe, Severe, Moderate) => Severe,       //63
                (Severe, Severe, Severe) => Severe,         //64
            };
    }
}
