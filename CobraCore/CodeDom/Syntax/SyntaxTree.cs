using System.Collections.Generic;
using System.Linq;

namespace CobraCore.CodeDom.Syntax
{
    /// <summary>
    /// Represents the overall structure - the syntax tree of the compiler
    /// </summary>
    public sealed class SyntaxTree
    {
        public IReadOnlyList<Diagnostic> Diagnostics { get; }
        public Expression Root { get; }
        public SyntaxToken EofToken { get; }

        public SyntaxTree(IReadOnlyList<Diagnostic> diagnostics, Expression root, SyntaxToken eofToken)
        {
            Diagnostics = diagnostics.ToArray();
            Root = root;
            EofToken = eofToken;
        }

        /// <summary>
        /// Initialize the parsing of the <see cref="SyntaxTree"/>
        /// </summary>
        /// <param name="text">The text to initialize the <see cref="SyntaxTree"/> with</param>
        /// <returns></returns>
        public static SyntaxTree Parse(string text)
            => new Parser(text).Parse();
    }
}
