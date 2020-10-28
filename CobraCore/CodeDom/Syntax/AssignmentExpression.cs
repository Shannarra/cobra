using System.Collections.Generic;

namespace CobraCore.CodeDom.Syntax
{
    public sealed class AssignmentExpression : Expression
    {
        public AssignmentExpression(SyntaxToken identifier, SyntaxToken equalsToken, Expression expression)
        {
            Identifier = identifier;
            EqualsToken = equalsToken;
            Expression = expression;
        }

        public SyntaxToken Identifier { get; }
        public SyntaxToken EqualsToken { get; }
        public Expression Expression { get; }

        public override SyntaxKind Kind => SyntaxKind.AssignmentExpression;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Identifier;
            yield return EqualsToken;
            yield return Expression;
        }
    }
}