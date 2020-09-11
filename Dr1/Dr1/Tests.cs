using NUnit.Framework;

namespace Dr1
{
    [TestFixture]
    public class Tests
    {
        [TestCase(1, "+", 2, 4)]
        [TestCase(-3, "+", 0.5, -2.5)]
        [TestCase(6, "*", 6, 36)]
        [TestCase(3, "*", 5, 15)]
        [TestCase(7, "-", 8, -1)]
        [TestCase(1.5, "-", 2, 3)]
        [TestCase(6, "/", 3, 2)]
        [TestCase(5, "/", 2, 2.5)]
        public void TestCases(double a, string @operator, double b, int expectedResult)
        {
            Assert.AreEqual(expectedResult, Calculator.Calculate(a, @operator, b));
        }
    }
}
