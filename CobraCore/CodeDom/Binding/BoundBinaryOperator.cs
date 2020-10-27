using System;
using System.Linq;
using CobraCore.CodeDom.Syntax;

namespace CobraCore.CodeDom.Binding
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

        private static readonly BoundBinaryOperator[] operators =
        {
            new BoundBinaryOperator(SyntaxKind.Plus, BoundBinaryOperatorKind.Addition, typeof(int)),                                // 3 + 3
            new BoundBinaryOperator(SyntaxKind.Minus, BoundBinaryOperatorKind.Subtraction, typeof(int)),                            // 3 - 3
            new BoundBinaryOperator(SyntaxKind.Star, BoundBinaryOperatorKind.Multiplication, typeof(int)),                          // 3 * 3
            new BoundBinaryOperator(SyntaxKind.Slash, BoundBinaryOperatorKind.Division, typeof(int)),                               // 3 / 3

            new BoundBinaryOperator(SyntaxKind.DoubleEquals, BoundBinaryOperatorKind.Equals, typeof(int), typeof(bool)),   // 3 == 3
            new BoundBinaryOperator(SyntaxKind.NotEquals, BoundBinaryOperatorKind.NotEquals, typeof(int), typeof(bool)),   // 3 != 3

            
            new BoundBinaryOperator(SyntaxKind.BooleanAnd, BoundBinaryOperatorKind.LogicalAnd, typeof(bool)),                       // true && false
            new BoundBinaryOperator(SyntaxKind.BooleanOr, BoundBinaryOperatorKind.LogicalOr, typeof(bool)),                         // true || false
            new BoundBinaryOperator(SyntaxKind.TextAndKeyword, BoundBinaryOperatorKind.LogicalAnd, typeof(bool)),                   // true and false
            new BoundBinaryOperator(SyntaxKind.TextOrKeyword, BoundBinaryOperatorKind.LogicalOr, typeof(bool)),                     // true or false
            new BoundBinaryOperator(SyntaxKind.DoubleEquals, BoundBinaryOperatorKind.Equals, typeof(bool)),                         // true == false
            new BoundBinaryOperator(SyntaxKind.IsTextBooleanKeyword, BoundBinaryOperatorKind.Equals, typeof(bool)),                 // true is false
            new BoundBinaryOperator(SyntaxKind.NotEquals, BoundBinaryOperatorKind.NotEquals, typeof(bool)),                         // true != false
            new BoundBinaryOperator(SyntaxKind.NegatedIsTextKeyword, BoundBinaryOperatorKind.NotEquals, typeof(bool)),              // true !is false


        };

        public static BoundBinaryOperator Bind(SyntaxKind kind, Type leftType, Type right)
        {
            return operators.FirstOrDefault(binary => binary.SyntaxKind == kind && binary.LeftType == leftType && binary.RightType == right);
        }
    }
}