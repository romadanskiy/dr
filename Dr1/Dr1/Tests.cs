using System;
using NUnit.Framework;

namespace Dr1
{
    [TestFixture]
    public class CalculatorTests
    {
        [TestCase(1, "+", 2, 3)]
        [TestCase(-3, "+", 0.5, -2.5)]
        [TestCase(6, "*", 6, 36)]
        [TestCase(3, "*", 5, 15)]
        [TestCase(7, "-", 8, -1)]
        [TestCase(1.5, "-", 2, -0.5)]
        [TestCase(6, "/", 3, 2)]
        [TestCase(5, "/", 2, 2.5)]
        [TestCase(1, "/", 0, double.PositiveInfinity)]
        public void CalculateTest(double a, string @operator, double b, double expectedResult)
        {
            Assert.AreEqual(expectedResult, Calculator.Calculate(a, @operator, b));
        }

        [Test]
        public void WrongOperator()
        {
            Assert.Throws<NotSupportedException>(() => Calculator.Calculate(8, "#", 5));
        }
        
    }
}
