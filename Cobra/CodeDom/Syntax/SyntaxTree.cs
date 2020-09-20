using System.Collections.Generic;
using System.Linq;

namespace Cobra.CodeDom.Syntax
{
    /// <summary>
    /// Represents the overall structure - the syntax tree of the compiler
    /// </summary>
    public sealed class SyntaxTree
    {
        public IReadOnlyList<string> Errors { get; }
        public Expression Root { get; }
        public SyntaxToken EofToken { get; }

        public SyntaxTree(IReadOnlyList<string> errors, Expression root, SyntaxToken eofToken)
        {
            Errors = errors.ToArray();
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
