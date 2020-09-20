using System.Collections.Generic;

namespace Cobra.Syntax
{
    public abstract class SyntaxNode
    {
        public abstract SyntaxKind Kind { get; }

        public abstract IEnumerable<SyntaxNode> GetChildren();
    }

    public abstract class Expression : SyntaxNode { }
}