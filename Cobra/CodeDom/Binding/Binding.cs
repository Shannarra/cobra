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
            var value = expression.Value ?? 0;
            return new BoundLiteralExpression(value);
        }

        private BoundExpression BindUnaryExpression(UnaryOperationExpressionSyntax expression)
        {
            var boundOperand = Bind(expression.Operand);
            var boundOperator = BoundUnaryOperator.Bind(expression.OperatorToken.Kind, boundOperand.Type);

            if (boundOperator == null)
            {
                errors.Add($"Unary operator {expression.OperatorToken.Text} is not defined for type {boundOperand.Type}");
                return boundOperand;
            }

            return new BoundUnaryExpression(boundOperator, boundOperand);
        }

        

        private BoundExpression BindBinaryExpression(BinaryOperationExpressionSyntax expression)
        {
            var boundLeftOperand = Bind(expression.Left);
            var boundRightOperand = Bind(expression.Right);
            var boundOperator = BoundBinaryOperator.Bind(expression.OperatorToken.Kind, boundLeftOperand.Type, boundRightOperand.Type);

            if (boundOperator == null)
            {
                errors.Add($"Binary operator {expression.OperatorToken.Text} is not defined for types {boundLeftOperand.Type} and {boundRightOperand.Type}");
                return boundLeftOperand;
            }

            return new BoundBinaryExpression(boundLeftOperand,  boundOperator, boundRightOperand);
        }
    }
}
