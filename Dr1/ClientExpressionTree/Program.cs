using System;
using Dr1;
using Microsoft.Extensions.DependencyInjection;
using static ClientExpressionTree.InputOutput;

namespace ClientExpressionTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = GetInputExpression();
            
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<ICalculator, WebCalculator>();
            var client = new Client(serviceCollection);
            
            var rpn = client.GetRPN(input);
            WriteRPN(rpn);
            var tree = client.GetExpressionTree(rpn);
            WriteTree(tree);
            var res = client.ProcessInParallelAsync(tree).Result;
            WriteResult(res);
        }
    }
}