using System;

namespace Dr1
{
    public class InputOutput
    {
        public static double GetNumber()
        {
            Console.WriteLine("Введите число:");
            var number = double.Parse(Console.ReadLine());
            return number;
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