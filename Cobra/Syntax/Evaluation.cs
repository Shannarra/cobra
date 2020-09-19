using System;

namespace Cobra.Syntax
{
    class Evaluator
    {
        public Expression Root { get; }

        public Evaluator(Expression root)
        {
            Root = root;
        }

        public int Evaluate()
        {
            return EvaluateExpression(Root);
        }

        private int EvaluateExpression(Expression root)
        {
            // binary expression
            if (root is BinaryOperationExpressionSyntax bin)
            {
                var left = EvaluateExpression(bin.Left);
                var right = EvaluateExpression(bin.Right);

                if (bin.OperatorToken.Kind == SyntaxKind.Plus)
                    return left + right;
                else if (bin.OperatorToken.Kind == SyntaxKind.Minus)
                    return left - right;
                else if (bin.OperatorToken.Kind == SyntaxKind.Star)
                    return left * right;
                else if (bin.OperatorToken.Kind == SyntaxKind.Slash)
                    return left / right;
                throw new Exception($"Unexpected operator {bin.OperatorToken.Kind}");
            }

            // number expression
            if (root is NumberExpressionSyntax num)
                return (int)num.NumberToken.Value;
            
            // parenthesis
            if (root is ParenthesizedExpression parethesized)
                return EvaluateExpression(parethesized.Expression);

            throw new Exception($"Unexpected node {root.Kind}");
        }
    }
}
