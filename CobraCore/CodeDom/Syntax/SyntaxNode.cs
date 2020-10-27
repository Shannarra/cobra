using System.Collections.Generic;

namespace CobraCore.CodeDom.Syntax
{
    /// <summary>
    /// Represents an expression node (leaf) of the <see cref="SyntaxTree"/>
    /// </summary>
    public abstract class SyntaxNode
    {
        /// <summary>
        /// Exposes the <see cref="SyntaxKind"/> of the current <see cref="SyntaxNode"/>
        /// </summary>
        public abstract SyntaxKind Kind { get; }

        /// <summary>
        /// Exposes all the child elements of the current <see cref="SyntaxNode"/>
        /// </summary>
        /// <returns><see cref="IEnumerable{SyntaxNode}" /></returns>
        public abstract IEnumerable<SyntaxNode> GetChildren();
    }

    /// <summary>
    /// <inheritdoc cref="SyntaxNode"/> wrapper
    /// </summary>
    public abstract class Expression : SyntaxNode { }
}