using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;

namespace ClientExpressionTree
{
    public class ExpressionTree
    {
        private static readonly Stack<Expression> expressions = new Stack<Expression>();
        private static readonly Stack<ConstantExpression> operations = new Stack<ConstantExpression>();

        public static double Calculate(Expression tree)
        {
            //ToDo
            throw new Exception();
        }
        
        public static Expression ParseInputExpression(string exp)
        {
            var arr = exp.ToCharArray();
            var index = 0;
            operations.Push(Expression.Constant('('));
            while (index <= arr.Length)
            {
                if (arr[index] >= '0' && arr[index] <= '9')
                {
                    var n = Convert.ToInt32(arr[index++]);
                    index++;
                    while (arr[index] >= '0' && arr[index] <= '9' && index < arr.Length)
                    {
                        n = n * 10 + (Convert.ToInt32(arr[index]));
                        index++;
                    }
                    expressions.Push(Expression.Constant(n));
                }
                else if (arr[index] == ')' || index == arr.Length)
                {
                    while (Convert.ToChar(operations.Peek().Value) != '(')
                    {
                        ExecuteOperation();
                    }
                    operations.Pop();
                    index++;
                }
                else
                {
                    while (operations.Count > 0 && CheckOperation(arr[index], Convert.ToChar(operations.Peek().Value)))
                    {
                        ExecuteOperation();
                    }
                    operations.Push(Expression.Constant(arr[index]));
                    index++;
                }
            }

            return expressions.Pop();
        }

        private static void ExecuteOperation()
        {
            var right = expressions.Pop();
            var left = expressions.Pop();
            var operation = operations.Pop();
            
            //ToDo
        }

        private static bool CheckOperation(char curOp, char prevOp)
        {
            var flag = false;
            switch (curOp)
            {
                case ')':
                    flag = true;
                    break;
                case '+':
                case '-':
                    flag = prevOp != '(';
                    break;
                case '*':
                case '/':
                    flag = prevOp == '*' || prevOp == '/';
                    break;
            }

            return flag;
        }

        private static string GetRequest(double num1, char operation, double num2)
        {
            return "http://localhost:5000/?num1=" + num1 + "&operation=" + operation + "&num2=" + num2;
        }

        private static double GetResponse(double num1, char operation, double num2)
        {
            var request = GetRequest(num1, operation, num2);
            var proxy = (HttpWebRequest)WebRequest.Create(request);
            var stream = proxy.GetResponse().GetResponseStream();
            var reader = new StreamReader(stream);
            var res = reader.ReadToEnd();

            return double.Parse(res);
        }
    }
}