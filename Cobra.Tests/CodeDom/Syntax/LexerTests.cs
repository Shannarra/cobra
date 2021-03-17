using CobraCore.CodeDom.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cobra.Tests.CodeDom.Syntax
{
    public class LexerTests
    {
        [Theory]
        [MemberData(nameof(GetTestTokensData))]
        public void LexerLexesSuccessfully(SyntaxKind kind, string text)
        {
            var tokens = SyntaxTree.ParseTokens(text);

            var token = Assert.Single(tokens);
            Assert.Equal(kind, token.Kind);
            Assert.Equal(text, token.Text);
        }
        
        [Theory]
        [MemberData(nameof(GetTestTokenPairsData))]
        public void LexerLexesPairsSuccessfully(SyntaxKind firstKind, string firstText,
            SyntaxKind secondKind, string secondText)
        {
            string text = firstText + secondText;
            var tokens = SyntaxTree.ParseTokens(text).ToArray();

            Assert.Equal(2, tokens.Length);
            Assert.Equal(tokens[0].Kind, firstKind);
            Assert.Equal(tokens[0].Text, firstText);
            Assert.Equal(tokens[1].Kind, secondKind);
        }
        
        [Theory]
        [MemberData(nameof(GetSeparatedPairsData))]
        public void LexerLexesSeparatedPairsSuccessfully(SyntaxKind firstKind, string firstText,
            SyntaxKind separatorKind, string separatorText,
            SyntaxKind secondKind, string secondText)
        {
            
            string text = firstText + separatorText + secondText;
            var tokens = SyntaxTree.ParseTokens(text).ToArray();
            

            Assert.Equal(3, tokens.Length);
            Assert.Equal(tokens[0].Kind, firstKind);
            Assert.Equal(tokens[0].Text, firstText);

            Assert.Equal(tokens[1].Kind, separatorKind);
            Assert.Equal(tokens[1].Text, separatorText);
            
            Assert.Equal(tokens[2].Kind, secondKind);
            Assert.Equal(tokens[2].Text, secondText);
        }

        public static IEnumerable<object[]> GetTestTokensData()
        {
            foreach (var (kind, text) in GetTestTokens().Concat(GetSeparatorTokens()))
                yield return new object[] { kind, text };
        }

        public static IEnumerable<object[]> GetTestTokenPairsData()
        {
            foreach (var token in GetTestTokenPairs())
                yield return new object[] { token.firstKind, token.firstText, token.secondKind, token.secondText };
        }
        
        public static IEnumerable<object[]> GetSeparatedPairsData()
        {
            foreach (var token in GetTestTokenPairsWithSeparators())
                yield return new object[] { token.firstKind, token.firstText, 
                                            token.separatorKind, token.separatorText, 
                                            token.secondKind, token.secondText };
        }

        private static bool RequiresSeparator(SyntaxKind left, SyntaxKind right)
        {
            var leftIsKeyword = left.ToString().EndsWith("Keyword");
            var rightIsKeyword = right.ToString().EndsWith("Keyword");


            if (left == SyntaxKind.Identifier || right == SyntaxKind.Identifier)
                return true;

            if (leftIsKeyword && rightIsKeyword)
                return true;

            if (leftIsKeyword && right == SyntaxKind.Identifier)
                return true;

            if (left == SyntaxKind.Identifier && rightIsKeyword)
                return true;

            if (left == SyntaxKind.Number && right == SyntaxKind.Number)
                return true;

            if (left == SyntaxKind.BooleanNot && right == SyntaxKind.EqualsToken)
                return true;

            if (left == SyntaxKind.EqualsToken && right == SyntaxKind.EqualsToken)
                return true;
            
            if (left == SyntaxKind.EqualsToken && right == SyntaxKind.DoubleEquals)
                return true;

            if (left == SyntaxKind.BooleanNot && right == SyntaxKind.DoubleEquals)
                return true;
            
            if (left == SyntaxKind.BooleanNot && right == SyntaxKind.IsTextBooleanKeyword)
                return true;

            return false;
        }


        private static IEnumerable<(SyntaxKind kind, string text)> GetTestTokens()
        {
            return new[]
            {
                (SyntaxKind.Identifier, "x"),
                (SyntaxKind.Identifier, "xyz"),
                (SyntaxKind.Number, "1"),
                (SyntaxKind.Number, "39"),

                (SyntaxKind.EqualsToken, "="),
                (SyntaxKind.BooleanNot, "!"),
                (SyntaxKind.Plus, "+"),
                (SyntaxKind.Minus, "-"),
                (SyntaxKind.Star, "*"),
                (SyntaxKind.Slash, "/"),
                (SyntaxKind.ParenthesisOpen, "("),
                (SyntaxKind.ParenthesisClose, ")"),
                (SyntaxKind.BooleanAnd, "&&"),
                (SyntaxKind.BooleanOr, "||"),
                (SyntaxKind.TextAndKeyword, "and"),
                (SyntaxKind.TextOrKeyword, "or"),
                (SyntaxKind.IsTextBooleanKeyword, "is"),
                (SyntaxKind.NegatedIsTextKeyword, "!is"),
                (SyntaxKind.FalseKeyword, "false"),
                (SyntaxKind.TrueKeyword, "true"),
                (SyntaxKind.NotEquals, "!="),
                (SyntaxKind.DoubleEquals, "==")
            };
        }

        private static IEnumerable<(SyntaxKind kind, string text)> GetSeparatorTokens()
        {
            return new[]
            {
                (SyntaxKind.WhiteSpace, " "),
                (SyntaxKind.WhiteSpace, "  "),
                (SyntaxKind.WhiteSpace, "\r"),
                (SyntaxKind.WhiteSpace, "\n"),
                (SyntaxKind.WhiteSpace, "\r\n")
            };
        }

        private static IEnumerable<(SyntaxKind firstKind, string firstText,
            SyntaxKind secondKind, string secondText)> GetTestTokenPairs()

        {
            foreach (var first in GetTestTokens())
            {
                foreach (var second in GetTestTokens())
                {
                    if (!RequiresSeparator(first.kind, second.kind)) //we don't need to separate the tokens
                        yield return (first.kind, first.text, second.kind, second.text); //so do your stuff
                }
            }
        }


        private static IEnumerable<(SyntaxKind firstKind, string firstText, 
            SyntaxKind separatorKind, string separatorText, 
            SyntaxKind secondKind, string secondText)> GetTestTokenPairsWithSeparators()
        {
            foreach (var first in GetTestTokens())
            {
                foreach (var second in GetTestTokens())
                {
                    if (RequiresSeparator(first.kind, second.kind))
                        foreach (var separator in GetSeparatorTokens())
                            yield return (first.kind, first.text, 
                                separator.kind, separator.text,
                                second.kind, second.text);
                }
            }
        }
    }
}
