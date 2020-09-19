using System.Collections.Generic;

namespace Cobra.Syntax
{
    sealed class BinaryOperationExpressionSyntax : Expression
    {
        public Expression Left { get; }
        public SyntaxToken OperatorToken { get; }
        public Expression Right { get; }
        public override SyntaxKind Kind => SyntaxKind.BinaryOperationExpression;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Left;
            yield return OperatorToken;
            yield return Right;
        }

        public BinaryOperationExpressionSyntax(Expression left, SyntaxToken operatorToken, Expression right)
        {
            Left = left;
            OperatorToken = operatorToken;
            Right = right;
        }
    }
}