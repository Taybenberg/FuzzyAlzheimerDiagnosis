using InferenceLibrary;
using System.Text.Json;

namespace InferenceTests
{
    [TestClass]
    public class DatasetTests
    {
        public class InputData
        {
            public double ML { get; set; }
            public double CS1 { get; set; }
            public double CS2 { get; set; }
            public Severity Severity { get; set; }
        }

        private readonly List<InputData> _inputData;

        public DatasetTests()
        {
            var content = File.ReadAllText("Dataset.json");
            _inputData = JsonSerializer.Deserialize<List<InputData>>(content)!;

            ModelCacheManager.InitializeModel();
        }

        [TestMethod]
        public void TestDataset()
        {
            foreach (var inputData in _inputData)
            {
                var input = new double[]
                {
                    inputData.ML,
                    inputData.CS1,
                    inputData.CS2,
                };

                var result = MamdaniInference.Process(input);

                Assert.AreEqual(result.wordResult.term, inputData.Severity);
            }
        }
    }
}
