using System;
using System.Collections.Generic;
using System.Text;

namespace CobraCore.CodeDom.Syntax
{
    /// <summary>
    /// <inheritdoc cref="Expression"/> providing the syntax for a binary expression
    /// </summary>
    public sealed class UnaryOperationExpressionSyntax : Expression
    {
        /// <summary>
        /// The operator, responsible for the expression
        /// </summary>
        public SyntaxToken OperatorToken { get; }

        /// <summary>
        /// The operand of the operator
        /// </summary>
        public Expression Operand { get; }

        public override SyntaxKind Kind => SyntaxKind.UnaryOperationExpression;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return OperatorToken;
            yield return Operand;
        }

        /// <summary>
        /// Creates new <see cref="BinaryOperationExpressionSyntax"/>
        /// </summary>
        /// <param name="left">Left side of the expression</param>
        /// <param name="operatorToken">The responsible operator</param>
        /// <param name="right">Right side of the expression</param>
        public UnaryOperationExpressionSyntax(SyntaxToken operatorToken, Expression operand)
        {
            OperatorToken = operatorToken;
            Operand = operand;
        }
    }
}
