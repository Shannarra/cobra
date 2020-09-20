using System;

namespace Cobra.CodeDom.Binding
{
    internal sealed class BoundBinaryExpression : BoundExpression
    {
        public BoundExpression Left { get; }
        public BoundBinaryOperator Operator { get; }
        public BoundExpression Right { get; }
        public override BoundNodeKind Kind => BoundNodeKind.BoundBinaryExpression;
        public override Type Type => Left.Type;

        public BoundBinaryExpression(BoundExpression left, BoundBinaryOperator op, BoundExpression right)
        {
            Left = left;
            Operator = op;
            Right = right;
        }
    }
}