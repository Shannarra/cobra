using System;
using System.Collections.Generic;
using System.Text;

namespace Cobra.CodeDom.Syntax
{
    public static class SyntaxRules
    {
        public static int GetBinaryOperatorPriority(this SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.Plus:
                case SyntaxKind.Minus:
                    return 1;
                case SyntaxKind.Star:
                case SyntaxKind.Slash:
                    return 2; // higher priority than +, -
                default:
                    return 0;
            }
        }
        
        public static int GetUnaryOperatorPriority(this SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.Plus:
                case SyntaxKind.Minus:
                    return 3;
                default:
                    return 0;
            }
        } 
    }
}
