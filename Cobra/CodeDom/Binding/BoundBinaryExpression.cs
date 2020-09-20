using System;

namespace Cobra.CodeDom.Binding
{
    internal sealed class BoundBinaryExpression : BoundExpression
    {
        public BoundExpression Left { get; }
        public BoundBinaryOperatorKind OperatorKind { get; }
        public BoundExpression Right { get; }
        public override BoundNodeKind Kind => BoundNodeKind.BoundBinaryExpression;
        public override Type Type => Left.Type;

        public BoundBinaryExpression(BoundExpression left, BoundBinaryOperatorKind kind, BoundExpression right)
        {
            Left = left;
            OperatorKind = kind;
            Right = right;
        }
    }
}