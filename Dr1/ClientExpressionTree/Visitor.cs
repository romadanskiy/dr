using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ClientExpressionTree
{
    public class Visitor : ExpressionVisitor
    {
        private readonly Dictionary<ExpressionNode, ExpressionNode[]> executeBefore = new Dictionary<ExpressionNode, ExpressionNode[]>();

        public Dictionary<ExpressionNode, ExpressionNode[]> GetExecuteBefore(Expression expression)
        {
            Visit(expression);
            return executeBefore;
        }
        
        protected override Expression VisitBinary(BinaryExpression binaryExpression)
        {
            Visit(binaryExpression.Left);
            Visit(binaryExpression.Right);

            var left = ExpressionNode.GetExpressionNode(binaryExpression.Left);
            var right = ExpressionNode.GetExpressionNode(binaryExpression.Right);
            
            executeBefore[ExpressionNode.GetExpressionNode(binaryExpression, Math.Max(left.Rank, right.Rank))] = new[] {left, right};

            return binaryExpression;
        }

        protected override Expression VisitConstant(ConstantExpression constantExpression)
        {
            executeBefore[ExpressionNode.GetExpressionNode(constantExpression, -1)] = new ExpressionNode[0];
            return constantExpression;
        }
    }
}