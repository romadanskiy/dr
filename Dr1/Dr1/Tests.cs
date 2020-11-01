using System;
using NUnit.Framework;

namespace Dr1
{
    [TestFixture]
    public class CalculatorTests
    {
        private static readonly Calculator calculator = new Calculator();
        
        [TestCase(1, "+", 2, 3, TestName = "1 + 2 = 3")]
        [TestCase(-3, "+", 0.5, -2.5, TestName = "-3 + 0.5 = -2.5")]
        [TestCase(6, "*", 6, 36, TestName = "6 + 6 = 36")]
        [TestCase(3, "*", 0.5, 1.5, TestName = "3 * 0.5 = 1.5")]
        [TestCase(7, "-", 8, -1, TestName = "7 - 8 = -1")]
        [TestCase(1.5, "-", 2, -0.5, TestName = "1.5 - 2 = -0.5")]
        [TestCase(6, "/", 3, 2, TestName = "6 / 3 = 2")]
        [TestCase(5, "/", 2, 2.5, TestName = "5 / 2 = 2.5")]
        public void CalculateTest(double num1, string operation, double num2, double expectedResult)
        {
            Assert.AreEqual(expectedResult, calculator.Calculate(num1, operation, num2));
        }

        [Test]
        public void DivisionByZero()
        {
            Assert.Throws<ArgumentException>(() => calculator.Calculate(1, "/", 0));
        }

        [Test]
        public void WrongOperator()
        {
            Assert.Throws<NotSupportedException>(() => calculator.Calculate(8, "#", 5));
        }
    }
}
