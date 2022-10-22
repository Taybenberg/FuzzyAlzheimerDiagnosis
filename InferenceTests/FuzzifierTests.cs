using InferenceLibrary;

namespace InferenceTests
{
    [TestClass]
    public class FuzzifierTests
    {
        public FuzzifierTests()
        {
            ModelCacheManager.InitializeModel();
        }

        [TestMethod]
        public void Test1()
        {
            var input = new double[] { 1, 3.5, 6.5 };

            var result = Fuzzifier.Fuzzification(input);

            Assert.AreEqual(result.Length, 3);

            var first = result[0];
            var second = result[1];
            var third = result[2];

            Assert.AreEqual(first[Severity.Normal], 1, 0.1);
            Assert.AreEqual(first[Severity.Mild], 0, 0.1);
            Assert.AreEqual(first[Severity.Moderate], 0, 0.1);
            Assert.AreEqual(first[Severity.Severe], 0, 0.1);

            Assert.AreEqual(second[Severity.Mild], 1, 0.1);
            Assert.AreEqual(second[Severity.Normal], 0, 0.1);
            Assert.AreEqual(second[Severity.Moderate], 0, 0.1);
            Assert.AreEqual(second[Severity.Severe], 0, 0.1);

            Assert.AreEqual(third[Severity.Moderate], 1, 0.1);
            Assert.AreEqual(third[Severity.Normal], 0, 0.1);
            Assert.AreEqual(third[Severity.Mild], 0, 0.1);
            Assert.AreEqual(third[Severity.Severe], 0, 0.1);
        }

        [TestMethod]
        public void Test2()
        {
            var input = new double[] { 10, 2.5, 5 };

            var result = Fuzzifier.Fuzzification(input);

            Assert.AreEqual(result.Length, 3);

            var first = result[0];
            var second = result[1];
            var third = result[2];

            Assert.AreEqual(first[Severity.Severe], 1, 0.1);
            Assert.AreEqual(first[Severity.Normal], 0, 0.1);
            Assert.AreEqual(first[Severity.Mild], 0, 0.1);
            Assert.AreEqual(first[Severity.Moderate], 0, 0.1);

            Assert.IsTrue(second[Severity.Normal] < 0.55);
            Assert.IsTrue(second[Severity.Mild] > 0.45);
            Assert.AreEqual(second[Severity.Moderate], 0, 0.1);
            Assert.AreEqual(second[Severity.Severe], 0, 0.1);

            Assert.IsTrue(third[Severity.Mild] < 0.55);
            Assert.IsTrue(third[Severity.Moderate] > 0.45);
            Assert.AreEqual(third[Severity.Normal], 0, 0.1);
            Assert.AreEqual(third[Severity.Severe], 0, 0.1);
        }
    }
}