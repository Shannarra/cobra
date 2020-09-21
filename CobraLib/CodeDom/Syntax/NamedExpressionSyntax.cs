using System;
using System.Collections.Generic;
using System.Text;

namespace CobraLib.CodeDom.Syntax
{
    class NamedExpressionSyntax : Expression
    {
        public SyntaxToken Identifier { get; }
        public override SyntaxKind Kind => SyntaxKind.NameExpression;
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Identifier;
        }

        public NamedExpressionSyntax(SyntaxToken identifier)
        {
            Identifier = identifier;
        }
    }
}
