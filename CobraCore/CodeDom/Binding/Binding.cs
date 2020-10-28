using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        private readonly Dictionary<VariableSymbol, object> variables;

        public Binder(Dictionary<VariableSymbol, object> variables)
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
            string name = expression.Identifier.Text;

            var existing = variables.Keys.FirstOrDefault(x => x.Name == name);

            if (existing != null)
                variables.Remove(existing);
            
            var variable = new VariableSymbol(name, boundExpr.Type);
            variables[variable] = null;

            return new BoundAssignmentExpression(variable, boundExpr);
        }

        private BoundExpression BindNameExpression(NameExpression expression)
        {
            var name = expression.Identifier.Text;
            var variable = variables.Keys.FirstOrDefault(x => x.Name == name);

            if (variable == null)
            {
                diagnostics.ReportUndefinedName(expression.Identifier.Span, name);
                return new BoundLiteralExpression(0);
            }

            return new BoundVariableExpression(variable);
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
