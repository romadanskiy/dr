using System;
using System.Linq.Expressions;

namespace ClientExpressionTree
{
    public class Visitor : ExpressionVisitor
    {
        protected override Expression VisitBinary(BinaryExpression node)
        {
            //ToDo
            throw new Exception();
        }
    }
}