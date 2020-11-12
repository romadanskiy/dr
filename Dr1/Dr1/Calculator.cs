using System;

namespace Dr1
{
    public class Calculator : ICalculator
    {
        public double Calculate(double num1, string operation, double num2)
        {
            if (operation == "/" && num2 == 0)
                throw new ArgumentException("Нельзя делить на ноль");
            
            return operation switch
            {
                "+" => num1 + num2,
                "-" => num1 - num2,
                "*" => num1 * num2,
                "/" => num1 / num2,
                _ => throw new NotSupportedException()
            };
        }
    }
}
