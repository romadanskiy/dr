using System;
using System.Linq.Expressions;
using System.Dynamic;

namespace ClientExpressionTree
{
    public abstract class DynamicExpressionVisitor
    {
        protected DynamicExpressionVisitor()
        {
        }

        protected virtual Expression Visit(Expression node)
        {
            return node == null ? null : VisitExpression((dynamic) node);
        }
        
        // to override
        protected virtual Expression VisitExpression(BinaryExpression node)
        {
            return node;
        }

        // to override
        protected virtual Expression VisitExpression(ConstantExpression node)
        {
            return node;
        }
    }
}