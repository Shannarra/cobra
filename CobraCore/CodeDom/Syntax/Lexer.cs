using System;
using System.Collections.Generic;

namespace CobraCore.CodeDom.Syntax
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

        private DiagnosticContainer diagnostics = new DiagnosticContainer();

        public Lexer(string text)
        {
            this.text = text;
        }

        /// <summary>
        /// An error list
        /// </summary>
        public DiagnosticContainer Diagnostics => diagnostics;

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
            
            var start = position;

            if (char.IsDigit(current)) //deal w/ numbers
            {
                while (char.IsDigit(current))
                    Next();

                var len = position - start;
                var newText = text.Substring(start, len);

                if (!int.TryParse(newText, out var val))
                    diagnostics.ReportInvalidNumber(new TextSpan(start, len), newText, typeof(int));

                return new SyntaxToken(SyntaxKind.Number, start, newText, val);
            }

            if (char.IsLetter(current))
            {
                while (char.IsLetter(current))
                    Next();

                var len = position - start;
                var newText = text.Substring(start, len);
                var kind = SyntaxRules.GetKeyWordKind(newText);

                return new SyntaxToken(kind, start, newText, null);
            }
            
            if (char.IsWhiteSpace(current))
            {
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
                    {
                        position += 2;
                        return new SyntaxToken(SyntaxKind.BooleanAnd, start, "&&", null);
                    }
                    break;
                case '|':
                    if (nextChar == '|')
                    {
                        position += 2;
                        return new SyntaxToken(SyntaxKind.BooleanOr, start, "||", null);
                    }
                    break;
                case 'a':
                    if (nextChar == 'n' && LookAhead(2) == 'd') //and
                    {
                        position += 3;
                        return new SyntaxToken(SyntaxKind.TextAndKeyword, start, "and", null);
                    }
                    break;
                case 'o':
                    if (nextChar == 'r') //or
                    {
                        position += 2;
                        return new SyntaxToken(SyntaxKind.TextAndKeyword, start, "or", null);
                    }
                    break;
                case 'i':
                    if (nextChar == 's') // is
                    {
                        position += 2;
                        return new SyntaxToken(SyntaxKind.IsTextBooleanKeyword, start, "is", null);
                    }
                    break;
                case '=':
                    if (nextChar == '=')
                    {
                        position += 2;
                        return new SyntaxToken(SyntaxKind.DoubleEquals, start, "==", null);
                    }
                    else
                    {
                        position++;
                        return new SyntaxToken(SyntaxKind.EqualsToken, start, "=", null);
                    }
                case '!':
                    if (nextChar == '=')
                    {
                        position += 2;
                        return new SyntaxToken(SyntaxKind.NotEquals, start, "!=", null);
                    }
                    else if (nextChar == 'i' && LookAhead(2) == 's')
                    {
                        position += 3;
                        return new SyntaxToken(SyntaxKind.NegatedIsTextKeyword, start, "!is", null);
                    }
                    else
                    {
                        position++;
                        return new SyntaxToken(SyntaxKind.BooleanNot, start, "!", null);
                    }
            }

            diagnostics.ReportBadChar(position, current);

            return new SyntaxToken(SyntaxKind.Error, position++, text.Substring(position - 1, 1), null);
        }
    }
}
