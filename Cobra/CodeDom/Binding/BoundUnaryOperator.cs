using System;
using System.Linq;
using Cobra.CodeDom.Syntax;

namespace Cobra.CodeDom.Binding
{
    internal sealed class BoundUnaryOperator
    {
        public SyntaxKind SyntaxKind { get; }
        public BoundUnaryOperatorKind Kind { get; }
        public Type OperandType { get; }
        public Type ResultType { get; }

        private BoundUnaryOperator(SyntaxKind syntaxKind, BoundUnaryOperatorKind kind, Type operandType, Type resultType)
        {
            SyntaxKind = syntaxKind;
            Kind = kind;
            OperandType = operandType;
            ResultType = resultType;
        }
        
        private BoundUnaryOperator(SyntaxKind syntaxKind, BoundUnaryOperatorKind kind, Type operandType)
            :this (syntaxKind, kind, operandType, operandType) {}

        private static BoundUnaryOperator[] operators =
        {
            new BoundUnaryOperator(SyntaxKind.BooleanNot, BoundUnaryOperatorKind.LogicalNegation, typeof(bool)),  
            new BoundUnaryOperator(SyntaxKind.Plus, BoundUnaryOperatorKind.Identity, typeof(int)),  
            new BoundUnaryOperator(SyntaxKind.Minus, BoundUnaryOperatorKind.Negation, typeof(int)),  
        };

        public static BoundUnaryOperator Bind(SyntaxKind kind, Type operandType)
        {
            return operators.FirstOrDefault(unaryOperator => unaryOperator.SyntaxKind == kind && unaryOperator.OperandType == operandType);
        }
    }
}