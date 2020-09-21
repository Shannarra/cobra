using System;
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

        private char current => LookAhead(0);

        private char nextChar => LookAhead(1);

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

        private char LookAhead(int offset)
        {
            var index = position + offset;

            return index >= text.Length ? '\0' : text[index];
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

            if (char.IsLetter(current))
            {
                var start = position;

                while (char.IsLetter(current))
                    Next();

                var len = position - start;
                var newText = text.Substring(start, len);
                var kind = SyntaxRules.GetKeyWordKind(newText);

                return new SyntaxToken(kind, start, text, null);
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
                case '&':
                    if (nextChar == '&')
                        return new SyntaxToken(SyntaxKind.BooleanAnd, position+=2, "&&", null);
                    break;
                case '|':
                    if (nextChar == '|') 
                        return new SyntaxToken(SyntaxKind.BooleanOr, position+=2, "||", null);
                    break;
                case 'a':
                    if (nextChar == 'n' && LookAhead(2) == 'd') //and
                        return new SyntaxToken(SyntaxKind.TextAndKeyword, position += 3, "and", null);
                    break;
                case 'o':
                    if (nextChar == 'r') //or
                        return new SyntaxToken(SyntaxKind.TextAndKeyword, position += 2, "or", null);
                    break;
                case 'i':
                    if (nextChar == 's') // is
                        return new SyntaxToken(SyntaxKind.IsTextBooleanKeyword, position += 2, "is", null);
                    break;
                case '=':
                    if (nextChar == '=')
                        return new SyntaxToken(SyntaxKind.DoubleEquals, position += 2, "==", null);
                    break;
                case '!':
                    if (nextChar == '=')
                        return new SyntaxToken(SyntaxKind.NotEquals, position += 2, "!=", null);
                    else if (nextChar == 'i' && LookAhead(2) == 's')
                        return new SyntaxToken(SyntaxKind.NegatedIsTextKeyword, position += 3, "!is",null);
                    else 
                        return new SyntaxToken(SyntaxKind.BooleanNot, position++, "!", null);
            }

            errors.Add($"[ERROR]: Bad character \"{current}\" at {position}");

            return new SyntaxToken(SyntaxKind.Error, position++, text.Substring(position - 1, 1), null);
        }
    }
}
