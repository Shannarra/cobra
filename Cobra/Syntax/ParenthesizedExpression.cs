using System.Collections.Generic;

namespace Cobra.Syntax
{
    sealed class ParenthesizedExpression : Expression
    {
        public SyntaxToken OpenParenthesisToken { get; }
        public Expression Expression { get; }
        public SyntaxToken CloseParenthesisToken { get; }
        public override SyntaxKind Kind => SyntaxKind.ParethesizedExpression;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return OpenParenthesisToken;
            yield return Expression;
            yield return CloseParenthesisToken;
        }

        public ParenthesizedExpression(SyntaxToken openParenthesisToken, Expression expression, SyntaxToken closeParenthesisToken)
        {
            OpenParenthesisToken = openParenthesisToken;
            Expression = expression;
            CloseParenthesisToken = closeParenthesisToken;
        }
    }
}