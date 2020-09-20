using System;

namespace Cobra.Syntax
{
    /// <summary>
    /// Evaluator for expressions
    /// </summary>
    public class Evaluator
    {
        /// <summary>
        /// Root expression to evaluate
        /// </summary>
        public Expression Root { get; }

        public Evaluator(Expression root)
        {
            Root = root;
        }

        /// <summary>
        /// Evaluates the given <see cref="Root"/>
        /// </summary>
        /// <returns></returns>
        public int Evaluate()
        {
            return EvaluateExpression(Root);
        }

        /// <summary>
        /// Evaluates the <paramref name="root"/>
        /// </summary>
        /// <param name="root">The root to start evaluation from</param>
        /// <returns></returns>
        private int EvaluateExpression(Expression root)
        {
            switch (root)
            {
                // binary expression
                case BinaryOperationExpressionSyntax bin:
                {
                    var left = EvaluateExpression(bin.Left);
                    var right = EvaluateExpression(bin.Right);

                    return bin.OperatorToken.Kind switch
                    {
                        SyntaxKind.Plus => left + right,
                        SyntaxKind.Minus => left - right,
                        SyntaxKind.Star => left * right,
                        SyntaxKind.Slash => left / right,
                        _ => throw new Exception($"Unexpected operator {bin.OperatorToken.Kind}")
                    };
                }
                // number expression
                case LiteralExpressionSyntax num:
                    return (int)num.LiteralToken.Value;
                // parenthesis
                case ParenthesizedExpression parenthesized:
                    return EvaluateExpression(parenthesized.Expression);
                default:
                    throw new Exception($"Unexpected node {root.Kind}");
            }
        }
    }
}
