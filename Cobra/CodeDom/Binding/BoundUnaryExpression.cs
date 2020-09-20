using System;

namespace Cobra.CodeDom.Binding
{
    internal sealed class BoundUnaryExpression : BoundExpression
    {
        public BoundUnaryOperatorKind OperatorKind { get; }
        public BoundExpression Operand { get; }
        public override BoundNodeKind Kind => BoundNodeKind.UnaryExpression;
        public BoundUnaryOperator Operator { get; }
        public override Type Type => Operand.Type;

        public BoundUnaryExpression(BoundUnaryOperator op, BoundExpression operand)
        {
            Operator = op;
            Operand = operand;
        }

    }
}