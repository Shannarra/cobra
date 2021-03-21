using CobraCore.CodeDom.Syntax;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cobra.Tests.CodeDom.Syntax
{
    public class SyntaxRulesTests
    {
        [Theory]
        [MemberData(nameof(GetSyntaxKindData))]
        public void GetSyntaxRuleText(SyntaxKind kind)
        {

            foreach (var val in GetSyntaxKindData())
            {
                var text = SyntaxRules.GetText(kind);

                if (text == null)
                    return;

                var tokens = SyntaxTree.ParseTokens(text);
                var token = Assert.Single(tokens);

                Assert.Equal(token.Kind, kind);
                Assert.Equal(token.Text, text);
            }
        }

        public static IEnumerable<object[]> GetSyntaxKindData()
        {
            var kinds = (SyntaxKind[])Enum.GetValues(typeof(SyntaxKind));

            foreach (var kind in kinds)
                yield return new object[] { kind };
        }
    }
}
