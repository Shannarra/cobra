using System;

namespace CobraCore.CodeDom.Binding
{
    internal sealed class BoundAssignmentExpression : BoundExpression
    {

        public BoundAssignmentExpression(VariableSymbol variable, BoundExpression expression)
        {
            Variable = variable;
            Expression = expression;
        }

        public VariableSymbol Variable { get; }
        public BoundExpression Expression { get; }

        public override Type Type => Expression.Type;

        public override BoundNodeKind Kind => BoundNodeKind.AssignmentExpression;
    }
}