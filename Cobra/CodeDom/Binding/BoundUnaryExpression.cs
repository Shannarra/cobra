using System;

namespace Cobra.CodeDom.Binding
{
    internal sealed class BoundUnaryExpression : BoundExpression
    {
        public BoundUnaryOperatorKind OperatorKind { get; }
        public BoundExpression Operand { get; }
        public override BoundNodeKind Kind => BoundNodeKind.UnaryExpression;
        public override Type Type => Operand.Type;

        public BoundUnaryExpression(BoundUnaryOperatorKind kind, BoundExpression operand)
        {
            OperatorKind = kind;
            Operand = operand;
        }
    }
}