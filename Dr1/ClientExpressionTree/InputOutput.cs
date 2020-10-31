using System;
using System.Linq.Expressions;
using static ClientExpressionTree.Operations;

namespace ClientExpressionTree
{
    public static class InputOutput
    {
        public static string GetInputExpression()
        {
            Console.WriteLine("Введите выражение:");
            var input = Console.ReadLine(); // (2+3)/12*7+8*9

            while (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Пустое выражение!");
                Console.WriteLine("Введите выражение заново:");
                input = Console.ReadLine();
            }

            return input.Replace(" ", "");
        }

        public static void WriteTree(Expression expression)
        {
            Console.WriteLine("\nПлан выполнения:");
            const char line = '-';
            var visitor = new Visitor();
            var executeBefore = visitor.GetExecuteBefore(expression);
            foreach (var exp in executeBefore.Keys)
            {
                switch (exp.Expression)
                {
                    case ConstantExpression _:
                        Console.WriteLine(exp.Expression);
                        break;
                    case BinaryExpression _:
                        var l = new string(line, exp.Rank * 3);
                        var op = GetOperation(exp.Expression);
                        Console.WriteLine($"{l} {op}");
                        break;
                }
            }
        }

        public static void WriteResult(double res)
        {
            Console.WriteLine($"\nРезультат: {res}");
        }

        public static void WriteRPN(string[] rpn)
        {
            Console.WriteLine("\nОбратная польская запись:");
            foreach (var e in rpn)
                Console.Write(e + " ");
            Console.WriteLine();
        }
    }
}