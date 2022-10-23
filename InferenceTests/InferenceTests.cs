using InferenceLibrary;

namespace InferenceTests
{
    [TestClass]
    public class InferenceTests
    {
        public InferenceTests()
        {
            ModelCacheManager.InitializeModel();
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TestOutputExceptionLessThan3()
        {
            var input = new double[2];

            var result = Fuzzifier.Fuzzification(input);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TestOutputExceptionMoreThan3()
        {
            var input = new double[4];

            var result = Fuzzifier.Fuzzification(input);
        }

        [TestMethod]
        public void TestNormalSeverity()
        {
            var input = new double[] { 1, 1, 1 };

            var result = MamdaniInference.Process(input);

            Assert.AreEqual(result.wordResult.term, Severity.Normal);
        }

        [TestMethod]
        public void TestMildSeverity()
        {
            var input = new double[] { 3.5, 3.5, 3.5 };

            var result = MamdaniInference.Process(input);

            Assert.AreEqual(result.wordResult.term, Severity.Mild);
        }

        [TestMethod]
        public void TestModerateSeverity()
        {
            var input = new double[] { 6.5, 6.5, 6.5 };

            var result = MamdaniInference.Process(input);

            Assert.AreEqual(result.wordResult.term, Severity.Moderate);
        }

        [TestMethod]
        public void TestSevereSeverity()
        {
            var input = new double[] { 9, 9, 9 };

            var result = MamdaniInference.Process(input);

            Assert.AreEqual(result.wordResult.term, Severity.Severe);
        }
    }
}