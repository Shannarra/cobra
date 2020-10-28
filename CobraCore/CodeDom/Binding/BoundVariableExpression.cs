using System;

namespace CobraCore.CodeDom.Binding
{
    internal class BoundVariableExpression : BoundExpression
    {

        public BoundVariableExpression(VariableSymbol variable)
        {
            Variable = variable;
        }

        public override BoundNodeKind Kind => BoundNodeKind.VariableExpression;

        public override Type Type => Variable.Type;

        public VariableSymbol Variable { get; }
    }
}