using System.Collections.Generic;

namespace Cobra.CodeDom.Syntax
{
    /// <summary>
    /// Lex-es all characters into tokens
    /// </summary>
    internal class Lexer
    {
        private readonly string text;

        private int position;

        private char current => position >= text.Length ? '\0' : text[position];

        private List<string> errors = new List<string>();

        public Lexer(string text)
        {
            this.text = text;
        }

        /// <summary>
        /// An error list
        /// </summary>
        public IEnumerable<string> Errors => errors;

        /// <summary>
        /// Advances the current <see cref="position"/>
        /// </summary>
        private void Next()
        {
            position++;
        }

        /// <summary>
        /// Starts the Lex-ing work
        /// </summary>
        /// <returns></returns>
        public SyntaxToken Lex()
        {
            if (position >= text.Length) 
                return new SyntaxToken(SyntaxKind.EndOfFile, position, "\0", null);

            if (char.IsDigit(current)) //deal w/ numbers
            {
                var start = position;

                while (char.IsDigit(current))
                    Next();

                var len = position - start;
                var newText = text.Substring(start, len);

                if (!int.TryParse(newText, out var val))
                    errors.Add($"The number {newText} cannot be represented as int");

                return new SyntaxToken(SyntaxKind.Number, start, newText, val);
            }
            
            if (char.IsWhiteSpace(current))
            {
                var start = position;

                while (char.IsWhiteSpace(current))
                    Next();

                var len = position - start;
                var newText = text.Substring(start, len);

                return new SyntaxToken(SyntaxKind.WhiteSpace, start, newText, null);
            }

            switch (current)
            {
                case '+':
                    return new SyntaxToken(SyntaxKind.Plus, position++, "+", null);
                case '-':
                    return new SyntaxToken(SyntaxKind.Minus, position++, "-", null);
                case '*':
                    return new SyntaxToken(SyntaxKind.Star, position++, "*", null);
                case '/':
                    return new SyntaxToken(SyntaxKind.Slash, position++, "/", null);
                case '(':
                    return new SyntaxToken(SyntaxKind.ParenthesisOpen, position++, "(", null);
                case ')':
                    return new SyntaxToken(SyntaxKind.ParenthesisClose, position++, ")", null);
            }

            errors.Add($"[ERROR]: Bad character \"{current}\" at {position}");

            return new SyntaxToken(SyntaxKind.Error, position++, text.Substring(position - 1, 1), null);
        }
    }
}
