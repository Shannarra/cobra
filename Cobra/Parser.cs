using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Index = System.Index;

namespace Cobra
{
    class Parser
    {
        private readonly SyntaxToken[] tokens;

        private int position;

        private List<string> errors = new List<string>();
        
        public IReadOnlyList<string> Errors => errors;

        public Parser(string text)
        {
            var lexer = new Lexer(text);
            var tokens = new List<SyntaxToken>();

            SyntaxToken token;

            do
            {
                token = lexer.NextToken();

                if (token.Kind != SyntaxKind.WhiteSpace &&
                    token.Kind != SyntaxKind.Error)
                    tokens.Add(token);
            } while (token.Kind != SyntaxKind.EndOfFile);

            this.tokens = tokens.ToArray();
            errors.AddRange(lexer.Errors);
        }

        private SyntaxToken NextToken()
        {
            var curr = Current;
            position++;
            return curr;
        }

        private SyntaxToken Match(SyntaxKind kind)
        {
            if (Current.Kind == kind)
                return NextToken();

            errors.Add($"[ERROR]: Unexpected token [{Current.Kind}], expected [{kind}]");
            return new SyntaxToken(kind, Current.Position, null, null);
        }

        private SyntaxToken LookAhead(int offset)
        {
            var index = position + offset;

            if (index >= tokens.Length)
                return tokens[^1];

            return tokens[index];
        }

        private SyntaxToken Current => LookAhead(0);

        public SyntaxTree Parse()
        {
            var exp = ParseExpression();
            var eofToken = Match(SyntaxKind.EndOfFile);
            return new SyntaxTree(errors, exp, eofToken);
        }

        public Expression ParseExpression()
        {
            var left = ParsePrimary();

            while (Current.Kind == SyntaxKind.Plus ||
                   Current.Kind == SyntaxKind.Minus)
            {
                var operatorToken = NextToken(); // we must have an operator here
                var right = ParsePrimary();
                left = new BinaryOperationExpressionSyntax(left, operatorToken, right);
            }

            return left;
        }

        private Expression ParsePrimary()
        {
            var numToken = Match(SyntaxKind.Number);
            return new NumberExpressionSyntax(numToken);
        }
    }
}
