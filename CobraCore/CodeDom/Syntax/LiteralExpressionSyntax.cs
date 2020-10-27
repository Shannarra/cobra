using System.Collections.Generic;

namespace CobraCore.CodeDom.Syntax
{
    /// <summary>
    /// <inheritdoc cref="Expression"/> providing the syntax for a literal expression
    /// </summary>
    public sealed class LiteralExpressionSyntax: Expression
    {
        public LiteralExpressionSyntax(SyntaxToken token)
            : this(token, token.Value) {}
        
        public LiteralExpressionSyntax(SyntaxToken token, object value)
        {
            LiteralToken = token; 
            Value = value;
        }

        public override SyntaxKind Kind => SyntaxKind.LiteralExpression;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return LiteralToken;
        }

        public SyntaxToken LiteralToken { get; }
        public object Value { get; }
    }
}