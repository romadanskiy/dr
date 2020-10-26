using System;
using System.Linq.Expressions;

namespace ClientExpressionTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите выражение:");
            var exp = Console.ReadLine(); // "(2+3)/12*7+8*9"

            if (string.IsNullOrEmpty(exp))
            {
                Console.WriteLine("Пустое выражение!");
                return;
            }

            var tree = ExpressionTree.ParseInputExpression(exp);

            Console.WriteLine("План выполнения:");
            Console.WriteLine(tree.ToString());

            var res = ExpressionTree.Calculate(tree);
            Console.WriteLine($"Результат: {res}");

        }
    }
}