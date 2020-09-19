using System;
using System.Collections.Generic;
using System.Text;

namespace Cobra
{
    class Lexer
    {
        private readonly string text;

        private int position;

        private char current => position >= text.Length ? '\0' : text[position];


        public Lexer(string text)
        {
            this.text = text;
        }

        private void Next()
        {
            position++;
        }

        public SyntaxToken NextToken()
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

                int.TryParse(newText, out var val);

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

            return new SyntaxToken(SyntaxKind.Error, position++, text.Substring(position -1, 1), null);
        }
    }
}
