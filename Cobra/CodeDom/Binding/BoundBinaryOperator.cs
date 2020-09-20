using System;
using System.Linq;
using Cobra.CodeDom.Syntax;

namespace Cobra.CodeDom.Binding
{
    internal sealed class BoundBinaryOperator
    {
        public SyntaxKind SyntaxKind { get; }
        public BoundBinaryOperatorKind Kind { get; }
        public Type ResultType { get; }
        public Type LeftType { get; }
        public Type RightType { get; }

        private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type leftType, Type rightType, Type resultType)
        {
            SyntaxKind = syntaxKind;
            Kind = kind;
            ResultType = resultType;
            LeftType = leftType;
            RightType = rightType;
        }

        private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type type)
            : this(syntaxKind, kind, type, type, type) {}
        
        private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type type, Type resultType)
            : this(syntaxKind, kind, type, type, resultType) {}

        private static BoundBinaryOperator[] operators =
        {
            new BoundBinaryOperator(SyntaxKind.Plus, BoundBinaryOperatorKind.Addition, typeof(int)),
            new BoundBinaryOperator(SyntaxKind.Minus, BoundBinaryOperatorKind.Subtraction, typeof(int)),
            new BoundBinaryOperator(SyntaxKind.Star, BoundBinaryOperatorKind.Multiplication, typeof(int)),
            new BoundBinaryOperator(SyntaxKind.Slash, BoundBinaryOperatorKind.Division, typeof(int)),

            new BoundBinaryOperator(SyntaxKind.DoubleEquals, BoundBinaryOperatorKind.Equals, typeof(int), typeof(bool)),
            new BoundBinaryOperator(SyntaxKind.NotEquals, BoundBinaryOperatorKind.NotEquals, typeof(int), typeof(bool)),

            
            new BoundBinaryOperator(SyntaxKind.BooleanAnd, BoundBinaryOperatorKind.LogicalAnd, typeof(bool)),
            new BoundBinaryOperator(SyntaxKind.BooleanOr, BoundBinaryOperatorKind.LogicalOr, typeof(bool)),
            new BoundBinaryOperator(SyntaxKind.DoubleEquals, BoundBinaryOperatorKind.Equals, typeof(bool)),
            new BoundBinaryOperator(SyntaxKind.NotEquals, BoundBinaryOperatorKind.NotEquals, typeof(bool)),


        };

        public static BoundBinaryOperator Bind(SyntaxKind kind, Type leftType, Type right)
        {
            return operators.FirstOrDefault(binary => binary.SyntaxKind == kind && binary.LeftType == leftType && binary.RightType == right);
        }
    }
}