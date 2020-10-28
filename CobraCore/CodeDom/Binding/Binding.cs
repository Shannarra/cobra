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
        private readonly DiagnosticContainer diagnostics = new DiagnosticContainer();
        private readonly Dictionary<string, object> variables;

        public Binder(Dictionary<string, object> variables)
        {
            this.variables = variables;
        }

        public DiagnosticContainer Diagnostics => diagnostics;

        public BoundExpression Bind(Expression expression)
        {
            switch (expression.Kind)
            {
                case SyntaxKind.ParenthesizedExpression:
                    return BindParenthesizedExpression(((ParenthesizedExpression)expression));
                case SyntaxKind.LiteralExpression:
                    return BindLiteralExpression((LiteralExpressionSyntax) expression);
                case SyntaxKind.NameExpression:
                    return BindNameExpression((NameExpression)expression);
                case SyntaxKind.AssignmentExpression:
                    return BindAssingmentExpression((AssignmentExpression)expression);
                case SyntaxKind.UnaryOperationExpression:
                    return BindUnaryExpression((UnaryOperationExpressionSyntax) expression);
                case SyntaxKind.BinaryOperationExpression:
                    return BindBinaryExpression((BinaryOperationExpressionSyntax) expression);
                default:
                    throw new Exception($"Unexpected expression {expression.Kind}");
            }
        }

        private BoundExpression BindAssingmentExpression(AssignmentExpression expression)
        {
            var boundExpr = Bind(expression.Expression);
            return new BoundAssignmentExpression(expression.Identifier.Text, boundExpr);
        }

        private BoundExpression BindNameExpression(NameExpression expression)
        {
            var name = expression.Identifier.Text; 
            if (!variables.TryGetValue(name, out var value))
            {
                diagnostics.ReportUndefinedName(expression.Identifier.Span, name);
                return new BoundLiteralExpression(0);
            }

            var type = value?.GetType()?? typeof(object);
            return new BoundVariableExpression(name, type);
        }

        private BoundExpression BindParenthesizedExpression(ParenthesizedExpression expression)
            => Bind(expression.Expression);

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
