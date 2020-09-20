using System.Collections.Generic;

namespace Cobra.Syntax
{
    public sealed class LiteralExpressionSyntax: Expression
    {
        public LiteralExpressionSyntax(SyntaxToken token)
        {
            LiteralToken = token;
        }

        public override SyntaxKind Kind => SyntaxKind.NumberExpression;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return LiteralToken;
        }

        public SyntaxToken LiteralToken { get; }
    }
}