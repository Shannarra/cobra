using System.Collections.Generic;

namespace CobraLib.CodeDom.Syntax
{
    class AssignmentExpression : Expression
    {
        public SyntaxToken Identifier { get; }
        public SyntaxToken EqToken { get; }
        public Expression Expression { get; }
        public override SyntaxKind Kind => SyntaxKind.AssignmentExpression;

        public AssignmentExpression(SyntaxToken identifier, SyntaxToken eqToken, Expression expression)
        {
            Identifier = identifier;
            EqToken = eqToken;
            Expression = expression;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Identifier;
            yield return EqToken;
            yield return Expression;
        }
    }
}