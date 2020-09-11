using System;
using static Dr1.Calculator;

namespace Dr1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите первое число:");
            var a = GetNumber();
            Console.WriteLine("Введите оператор (+ - * /):");
            var @operator = Console.ReadLine();
            Console.WriteLine("Введите второе число:");
            var b = GetNumber();
            var res = Calculate(a, @operator, b);
            Console.WriteLine("Результат: {0}", res);
        }
    }
}
