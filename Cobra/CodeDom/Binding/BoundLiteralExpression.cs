using System;

namespace Cobra.CodeDom.Binding
{
    internal sealed class BoundLiteralExpression : BoundExpression
    {
        public override BoundNodeKind Kind => BoundNodeKind.LiteralExpression;
        public override Type Type => Value.GetType();
        public object Value { get; }

        public BoundLiteralExpression(object value)
        {
            Value = value;
        }
    }
}