using System.Collections.Generic;
using System.Linq;

namespace Cobra.Syntax
{
    public sealed class SyntaxToken : SyntaxNode
    {
        public override SyntaxKind Kind { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
            => Enumerable.Empty<SyntaxNode>();

        public int Position { get; }
        public string Text { get; }
        public object Value { get; }

        public SyntaxToken(SyntaxKind kind, int position, string text, object value)
        {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }
    }
}