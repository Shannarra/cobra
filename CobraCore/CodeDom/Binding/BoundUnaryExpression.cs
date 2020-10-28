using System;

namespace CobraCore.CodeDom.Binding
{
    internal sealed class BoundUnaryExpression : BoundExpression
    {
        public BoundUnaryOperatorKind OperatorKind => Operator.Kind;
        public BoundExpression Operand { get; }
        public override BoundNodeKind Kind => BoundNodeKind.UnaryExpression;
        public BoundUnaryOperator Operator { get; }
        public override Type Type => Operator.ResultType;

        public BoundUnaryExpression(BoundUnaryOperator op, BoundExpression operand)
        {
            Operator = op;
            Operand = operand;
        }

    }
}