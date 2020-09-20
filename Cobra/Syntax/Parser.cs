using System.Collections.Generic;

namespace Cobra.Syntax
{
    internal sealed class Parser
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

        private SyntaxToken MatchToken(SyntaxKind kind)
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
            var exp = ParseTermExpression();
            // if for example we pass "1 1", we expect "1 + 1", or just "1 \0", so we get an error - expected EOF
            var eofToken = MatchToken(SyntaxKind.EndOfFile); 
            return new SyntaxTree(errors, exp, eofToken);
        }
        
        private Expression ParsePrimary()
        {
            if (Current.Kind == SyntaxKind.ParenthesisOpen)
            {
                var left = NextToken();
                var expr = ParseTermExpression();
                var right = MatchToken(SyntaxKind.ParenthesisClose);
                return new ParenthesizedExpression(left, expr, right);
            }

            var numToken = MatchToken(SyntaxKind.Number);
            return new NumberExpressionSyntax(numToken);
        }

        public Expression ParseTermExpression()
        {
            var left = ParseMultiplicationExpression();

            while (Current.Kind == SyntaxKind.Plus ||
                   Current.Kind == SyntaxKind.Minus)
            {
                var operatorToken = NextToken(); // we must have an operator here
                var right = ParseMultiplicationExpression();
                left = new BinaryOperationExpressionSyntax(left, operatorToken, right);
            }

            return left;
        }
        
        public Expression ParseMultiplicationExpression()
        {
            var left = ParsePrimary();

            while (Current.Kind == SyntaxKind.Star ||
                   Current.Kind == SyntaxKind.Slash)
            {
                var operatorToken = NextToken(); // we must have an operator here
                var right = ParsePrimary();
                left = new BinaryOperationExpressionSyntax(left, operatorToken, right);
            }

            return left;
        }

        
    }
}
