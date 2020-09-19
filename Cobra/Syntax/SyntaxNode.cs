using System.Collections.Generic;

namespace Cobra.Syntax
{
    abstract class SyntaxNode
    {
        public abstract SyntaxKind Kind { get; }

        public abstract IEnumerable<SyntaxNode> GetChildren();
    }

    abstract class Expression : SyntaxNode { }
}