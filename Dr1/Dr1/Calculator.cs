using System;

namespace Dr1
{
    public class Calculator
    {
        public static int Calculate(int a, string @operator, int b)
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

        public static int GetNumber()
        {
            return int.Parse(Console.ReadLine());
        }
    }
}
