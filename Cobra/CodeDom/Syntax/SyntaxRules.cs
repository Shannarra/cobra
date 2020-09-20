using System;
using System.Collections.Generic;
using System.Text;

namespace Cobra.CodeDom.Syntax
{
    public static class SyntaxRules
    {
        public static int GetUnaryOperatorPriority(this SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.Plus:
                case SyntaxKind.Minus:
                case SyntaxKind.BooleanNot:
                    return 5;
                default:
                    return 0;
            }
        }
        
        public static int GetBinaryOperatorPriority(this SyntaxKind kind)
        {
            switch (kind)
            {
                
                case SyntaxKind.Star:
                case SyntaxKind.Slash:
                    return 4; // higher priority than +, -
                
                case SyntaxKind.Plus:
                case SyntaxKind.Minus:
                    return 3;
                
                case SyntaxKind.BooleanAnd:
                    return 2;

                case SyntaxKind.BooleanOr:
                    return 1;
                         
                default:
                    return 0;
            }
        }
        
        

        public static SyntaxKind GetKeyWordKind(string newText)
        {
            switch (newText)
            {
                case "true":
                    return SyntaxKind.TrueKeyword;
                case "false":
                    return SyntaxKind.FalseKeyword;
                default:
                    return SyntaxKind.Identifier;
            }
        }
    }
}
