using System;

namespace Dr1
{
    public class Calculator
    {
        public static double Calculate(double a, string @operator, double b)
        {
            return @operator switch
            {
                "+" => a + b,
                "-" => a - b,
                "*" => a * b,
                "/" => a / b,
                _ => throw new NotSupportedException()
            };
        }
    }
}
