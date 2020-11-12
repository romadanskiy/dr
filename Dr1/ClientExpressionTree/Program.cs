using System;
using static ClientExpressionTree.InputOutput;
using static ClientExpressionTree.Calculator;

namespace ClientExpressionTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = GetInputExpression();
            var rpn = GetRPN(input);
            WriteRPN(rpn);
            var tree = GetExpressionTree(rpn);
            WriteTree(tree);
            var res = ProcessInParallelAsync(tree).Result;
            WriteResult(res);
        }
    }
}