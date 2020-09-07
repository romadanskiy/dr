using System;
using static Dr1.Calculator;

namespace Dr1
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = GetNumber();
            var @operator = Console.ReadLine();
            var b = GetNumber();
            var res = Calculate(a, @operator, b);
            Console.WriteLine(res);
        }
    }
}
