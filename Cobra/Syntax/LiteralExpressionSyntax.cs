using System.Collections.Generic;

namespace Cobra.Syntax
{
    /// <summary>
    /// <inheritdoc cref="Expression"/> providing the syntax for a literal expression
    /// </summary>
    public sealed class LiteralExpressionSyntax: Expression
    {
        public LiteralExpressionSyntax(SyntaxToken token)
        {
            LiteralToken = token;
        }

        public override SyntaxKind Kind => SyntaxKind.LiteralExpression;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return LiteralToken;
        }

        public SyntaxToken LiteralToken { get; }
    }
}