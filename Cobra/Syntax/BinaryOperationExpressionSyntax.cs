using System.Collections.Generic;

namespace Cobra.Syntax
{
    /// <summary>
    /// <inheritdoc cref="Expression"/> providing the syntax for a binary expression
    /// </summary>
    public sealed class BinaryOperationExpressionSyntax : Expression
    {
        /// <summary>
        /// The left side of the expression
        /// </summary>
        public Expression Left { get; }

        /// <summary>
        /// The operator, responsible for the expression
        /// </summary>
        public SyntaxToken OperatorToken { get; }

        /// <summary>
        /// The right side of the expression
        /// </summary>
        public Expression Right { get; }

        public override SyntaxKind Kind => SyntaxKind.BinaryOperationExpression;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Left;
            yield return OperatorToken;
            yield return Right;
        }

        /// <summary>
        /// Creates new <see cref="BinaryOperationExpressionSyntax"/>
        /// </summary>
        /// <param name="left">Left side of the expression</param>
        /// <param name="operatorToken">The responsible operator</param>
        /// <param name="right">Right side of the expression</param>
        public BinaryOperationExpressionSyntax(Expression left, SyntaxToken operatorToken, Expression right)
        {
            Left = left;
            OperatorToken = operatorToken;
            Right = right;
        }
    }
}