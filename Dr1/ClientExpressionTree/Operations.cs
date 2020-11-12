using System;
using System.Linq;
using System.Linq.Expressions;

namespace ClientExpressionTree
{
    public static class Operations
    {
        public static readonly string[] FirstPriority = {"*", "/"};
        public static readonly string[] SecondPriority = {"+", "-"};
        
        public static Expression GetExpression(Expression left, string operation, Expression right)
        {
            return operation switch
            {
                "+" => Expression.Add(left, right),
                "-" => Expression.Subtract(left, right),
                "*" => Expression.Multiply(left, right),
                "/" => Expression.Divide(left, right),
                _ => throw new ArgumentException()
            };
        }
        
        public static string GetOperation(Expression expression)
        {
            return expression.NodeType switch
            {
                ExpressionType.Add => "+",
                ExpressionType.Subtract => "-",
                ExpressionType.Multiply => "*",
                ExpressionType.Divide => "/",
                _ => throw new ArgumentException()
            };
        }
        
        public static bool IsOperation(string c)
        {
            return FirstPriority.Contains(c) || SecondPriority.Contains(c);
        }
    }
}