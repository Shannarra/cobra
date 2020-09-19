using System.Collections.Generic;
using System.Linq;

namespace Cobra.Syntax
{
    class SyntaxTree
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

        public static SyntaxTree Parse(string text)
            => new Parser(text).Parse();
    }
}
