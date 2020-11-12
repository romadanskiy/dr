using System.Collections.Generic;
using System.Linq.Expressions;

namespace ClientExpressionTree
{
    public class ExpressionNode
    {
        private static readonly Dictionary<Expression, ExpressionNode> instances = new Dictionary<Expression, ExpressionNode>();
        
        public Expression Expression { get; }
        public double Result { get; set; }
        public int Rank { get; private set; }

        private ExpressionNode(Expression expression)
        {
            Expression = expression;
            if (expression is ConstantExpression constantExpression)
                Result = (double) constantExpression.Value;
        }

        public static ExpressionNode GetExpressionNode(Expression expression, int previousRanking)
        {
            if (instances.ContainsKey(expression))
                return instances[expression];

            var newNode = new ExpressionNode(expression) {Rank = previousRanking + 1};
            instances[expression] = newNode;
            return newNode;
        }
        
        public static ExpressionNode GetExpressionNode(Expression expression)
        {
            if (instances.ContainsKey(expression))
                return instances[expression];

            var newNode = new ExpressionNode(expression);
            instances[expression] = newNode;
            return newNode;
        }
    }
}