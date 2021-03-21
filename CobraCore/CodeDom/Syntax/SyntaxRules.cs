using System;
using System.Collections.Generic;
using System.Text;

namespace CobraCore.CodeDom.Syntax
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
                    return 5; // higher priority than +, -
                
                case SyntaxKind.Plus:
                case SyntaxKind.Minus:
                    return 4;
                
                case SyntaxKind.DoubleEquals: //priorities are SUPER important!!!!
                case SyntaxKind.NotEquals:
                case SyntaxKind.IsTextBooleanKeyword: // is  
                case SyntaxKind.NegatedIsTextKeyword: // !is
                    return 3;

                case SyntaxKind.BooleanAnd:
                case SyntaxKind.TextAndKeyword:
                    return 2;

                case SyntaxKind.BooleanOr:
                case SyntaxKind.TextOrKeyword:
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
                case "and":
                    return SyntaxKind.TextAndKeyword;
                case "or":
                    return SyntaxKind.TextOrKeyword;
                case "is":
                    return SyntaxKind.IsTextBooleanKeyword;
                case "!is":
                    return SyntaxKind.NegatedIsTextKeyword;
                default:
                    return SyntaxKind.Identifier;
            }
        }

        public static string GetText(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.WhiteSpace:
                    return " ";
                case SyntaxKind.EqualsToken:
                    return "=";
                case SyntaxKind.BooleanNot:
                    return "!";
                case SyntaxKind.Plus:
                    return "+";
                case SyntaxKind.Minus:
                    return "-";
                case SyntaxKind.Star:
                    return "*";
                case SyntaxKind.Slash:
                    return "/";
                case SyntaxKind.ParenthesisOpen:
                    return "(";
                case SyntaxKind.ParenthesisClose:
                    return ")";
                case SyntaxKind.BooleanAnd:
                    return "&&";
                case SyntaxKind.BooleanOr:
                    return "||";
                case SyntaxKind.TextAndKeyword:
                    return "and";
                case SyntaxKind.TextOrKeyword:
                    return "or";
                case SyntaxKind.IsTextBooleanKeyword:
                    return "is";
                case SyntaxKind.NegatedIsTextKeyword:
                    return "!is";
                case SyntaxKind.FalseKeyword:
                    return "false";
                case SyntaxKind.TrueKeyword:
                    return "true";
                case SyntaxKind.NotEquals:
                    return "!=";
                case SyntaxKind.DoubleEquals:
                    return "==";
                default:
                    return null;
            }
        }
    }
}
