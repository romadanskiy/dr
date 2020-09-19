using System;

namespace Dr1
{
    public class InputOutput
    {
        public static double GetNumber1()
        {
            Console.WriteLine("Введите первое число:");
            var a = double.Parse(Console.ReadLine());
            return a;
        }
        
        public static double GetNumber2()
        {
            Console.WriteLine("Введите первое число:");
            var a = double.Parse(Console.ReadLine());
            return a;
        }

        public static string GetOperator()
        {
            Console.WriteLine("Введите оператор (+ - * /):");
            return Console.ReadLine();
        }

        public static void ShowResult(double res)
        {
            Console.WriteLine("Результат: {0}", res);
        }
    }
}