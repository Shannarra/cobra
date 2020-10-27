using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using CobraCore.CodeDom.Syntax;

namespace CobraCore.CodeDom.Binding
{
    /// <summary>
    /// Walks the syntax tree, implements "type checker"
    /// </summary>
    internal sealed class Binder
    {
        private DiagnosticContainer diagnostics = new DiagnosticContainer();

        public DiagnosticContainer Diagnostics => diagnostics;

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
                case SyntaxKind.ParenthesizedExpression:
                    return Bind(((ParenthesizedExpression)expression).Expression);                
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
                diagnostics.ReportUndefinedUnaryOperator(expression.OperatorToken.Span, expression.OperatorToken.Text, boundOperand.Type);
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
                diagnostics.ReportUndefinedBinaryOperator(expression.OperatorToken.Span, expression.OperatorToken.Text, boundLeftOperand.Type, boundRightOperand.Type);
                return boundLeftOperand;
            }

            return new BoundBinaryExpression(boundLeftOperand,  boundOperator, boundRightOperand);
        }
    }
}
