using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Cobra.CodeDom.Syntax;

namespace Cobra.CodeDom.Binding
{
    /// <summary>
    /// Walks the syntax tree, implements "type checker"
    /// </summary>
    internal sealed class Binder
    {
        private List<string> errors = new List<string>();

        public IReadOnlyList<string> Errors => errors;

        public BoundExpression Bind(Expression expression)
        {
            switch (expression.Kind)
            {
                case SyntaxKind.LiteralExpression:
                    return BindLiteralExpression((LiteralExpressionSyntax) expression);
                case SyntaxKind.UnaryOperationExpression:
                    return BindUnaryExpression((UnaryOperationExpressionSyntax) expression);
                case SyntaxKind.BinaryOperationExpression:
                    return BindBinaryExpression((BinaryOperationExpressionSyntax) expression);
                default:
                    throw new Exception($"Unexpected expression {expression.Kind}");
            }
        }

        private BoundExpression BindLiteralExpression(LiteralExpressionSyntax expression)
        {
            var value = expression.LiteralToken.Value as int? ?? 0;
            return new BoundLiteralExpression(value);
        }

        private BoundExpression BindUnaryExpression(UnaryOperationExpressionSyntax expression)
        {
            var boundOperand = Bind(expression.Operand);
            var boundOperatorKind = BindUnaryOperatorKind(expression.OperatorToken.Kind, boundOperand.Type);

            if (boundOperatorKind == null)
            {
                errors.Add($"Unary operator {expression.OperatorToken.Text} is not defined for type {boundOperand.Type}");
                return boundOperand;
            }

            return new BoundUnaryExpression(boundOperatorKind.Value, boundOperand);
        }

        

        private BoundExpression BindBinaryExpression(BinaryOperationExpressionSyntax expression)
        {
            var boundLeftOperand = Bind(expression.Left);
            var boundRightOperand = Bind(expression.Right);
            var boundOperatorKind = BindBinaryOperatorKind(expression.OperatorToken.Kind, boundLeftOperand.Type, boundRightOperand.Type);

            if (boundOperatorKind == null)
            {
                errors.Add($"Binary operator {expression.OperatorToken.Text} is not defined for types {boundLeftOperand.Type} and {boundRightOperand.Type}");
                return boundLeftOperand;
            }

            return new BoundBinaryExpression(boundLeftOperand, boundOperatorKind.Value, boundRightOperand);
        }
        
        private BoundUnaryOperatorKind? BindUnaryOperatorKind(SyntaxKind expressionKind, Type boundOperandType)
        {
            if (boundOperandType != typeof(int))
                return null;


            switch (expressionKind)
            {
                case SyntaxKind.Plus:
                    return BoundUnaryOperatorKind.Identity;
                case SyntaxKind.Minus:
                    return BoundUnaryOperatorKind.Negation;
                default:
                    throw new Exception($"Unexpected expression of kind [{expressionKind}]");
            }
        }

        private BoundBinaryOperatorKind? BindBinaryOperatorKind(SyntaxKind expressionKind, Type leftType, Type rightType)
        {
            if (leftType != typeof(int) && rightType != typeof(int))
                return null;

            switch (expressionKind)
            {
                case SyntaxKind.Plus:
                    return BoundBinaryOperatorKind.Addition;
                case SyntaxKind.Minus:
                    return BoundBinaryOperatorKind.Subtraction;
                case SyntaxKind.Star:
                    return BoundBinaryOperatorKind.Multiplication;
                case SyntaxKind.Slash:
                    return BoundBinaryOperatorKind.Division;
                default:
                    throw new Exception($"Unexpected expression of kind [{expressionKind}]");
            }
        }
    }
}
