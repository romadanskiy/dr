using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using static ClientExpressionTree.Operations;

namespace ClientExpressionTree
{
    public static class Calculator
    {
        // Reverse Polish Notation (RPN) - обратная польская запись
        public static string[] GetRPN(string str)
        {
            var resultList = new List<string>();
            var operationsStack = new Stack<string>();

            for (var i = 0; i < str.Length; i++)
            {
                var symbol = str[i].ToString();

                if (char.IsDigit(str[i]))
                {
                    i = ProcessNumber(i, symbol, str, resultList);
                }
                else if (symbol == "(")
                {
                    operationsStack.Push(symbol);
                }
                else if (symbol == ")")
                {
                    while (operationsStack.Peek() != "(")
                        resultList.Add(operationsStack.Pop());
                    
                    operationsStack.Pop();
                }
                else if (IsOperation(symbol))
                {
                    ProcessOperation(symbol, resultList, operationsStack);
                }
                else
                {
                    throw new ArgumentException();
                }
            }

            while (operationsStack.Count > 0)
                resultList.Add(operationsStack.Pop());

            return resultList.ToArray();
        }

        private static int ProcessNumber(int i, string symbol, string str, List<string> resultList)
        {
            var n = symbol;
            while (i+1 < str.Length && char.IsDigit(str[i+1]))
            {
                n += str[i+1];
                i++;
            }
            resultList.Add(n);

            return i;
        }

        private static void ProcessOperation(string symbol, List<string> resultList, Stack<string> operationsStack)
        {
            if (SecondPriority.Contains(symbol))
            {
                while (operationsStack.Count > 0 && IsOperation(operationsStack.Peek()))
                {
                    resultList.Add(operationsStack.Pop());
                }
            }
            else
            {
                while (operationsStack.Count > 0 && FirstPriority.Contains(operationsStack.Peek()))
                {
                    resultList.Add(operationsStack.Pop());
                }
            }
            operationsStack.Push(symbol);
        }

        private static bool IsNum(string str)
        {
            return double.TryParse(str, out _);
        }

        // Возвращает Expression по обратной польской записи
        public static Expression GetExpressionTree(string[] rpn)
        {
            var stack = new Stack<Expression>();
            foreach (var element in rpn)
            {
                if (IsNum(element))
                    stack.Push(Expression.Constant(double.Parse(element)));
                else if (IsOperation(element))
                {
                    var operation = element;
                    var right = stack.Pop();
                    var left = stack.Pop();
                    var newExpression = GetExpression(left, operation, right);
                    stack.Push(newExpression);
                }
                else
                    throw new ArgumentException();
            }

            return stack.Pop();
        }

        // супер-метод Димы Иванова
        public static async Task<double> ProcessInParallelAsync(Expression expression)
        {
            var visitor = new Visitor();
            var lazy = new Dictionary<ExpressionNode, Lazy<Task>>();
            var executeBefore = visitor.GetExecuteBefore(expression);
            var res = executeBefore.Last().Key;
            
            foreach (var (exp, exps) in executeBefore)
            {
                lazy[exp] = new Lazy<Task>(async () =>
                {
                    await Task.WhenAll(exps.Select(e => lazy[e].Value));
                    await Task.Yield();
                    
                    if (exp.Expression is BinaryExpression)
                    {
                        var client = new HttpClient();
                        var num1 = exps[0].Result;
                        var operation = GetOperation(exp.Expression);
                        var num2 = exps[1].Result;
                        var request = GetRequest(num1, operation, num2);
                        var response = await client.GetAsync(request);
                        var result = response.Content.ReadAsStringAsync().Result;
                        exp.Result = double.Parse(result);
                    }
                });
            }

            await Task.WhenAll(lazy.Values.Select(l => l.Value));
            return res.Result;
        }

        private static string GetRequest(double num1, string operation, double num2)
        {
            operation = operation == "+" ? "%2b" : operation;
            return $"http://localhost:5000/?num1={num1}&operation={operation}&num2={num2}";
        }
    }
}