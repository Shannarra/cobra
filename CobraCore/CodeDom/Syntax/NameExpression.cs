using System.Collections.Generic;

namespace CobraCore.CodeDom.Syntax
{
    public sealed class NameExpression : Expression
    {
        public NameExpression(SyntaxToken identifier)
        {
            Identifier = identifier;
        }

        public SyntaxToken Identifier { get; }

        public override SyntaxKind Kind => SyntaxKind.NameExpression;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Identifier;
        }
    }
}