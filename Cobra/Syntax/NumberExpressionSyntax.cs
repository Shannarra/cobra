using System.Collections.Generic;

namespace Cobra.Syntax
{
    sealed class NumberExpressionSyntax : Expression
    {
        public NumberExpressionSyntax(SyntaxToken token)
        {
            NumberToken = token;
        }

        public override SyntaxKind Kind => SyntaxKind.NumberExpression;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return NumberToken;
        }

        public SyntaxToken NumberToken { get; }
    }
}