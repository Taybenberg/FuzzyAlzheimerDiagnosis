namespace FuzzyAlzheimerUI
{
    public record CognitiveAbility
    {
        public string Name { get; set; }
        public string CognitiveTest { get; set; }

        public CognitiveAbility(string name, string cognitiveTest)
        {
            Name = name;
            CognitiveTest = cognitiveTest;
        }
    }
}
