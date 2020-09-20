using System;
using Cobra.CodeDom.Binding;
using Cobra.CodeDom.Syntax;

namespace Cobra.CodeDom
{
    /// <summary>
    /// Evaluator for expressions
    /// </summary>
    internal class Evaluator
    {
        /// <summary>
        /// Root expression to evaluate
        /// </summary>
        public BoundExpression Root { get; }

        public Evaluator(BoundExpression root)
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
        private int EvaluateExpression(BoundExpression root)
        {
            switch (root)
            {
                // binary expression
                case BoundBinaryExpression bin:
                {
                    var left = EvaluateExpression(bin.Left);
                    var right = EvaluateExpression(bin.Right);

                    return bin.OperatorKind switch
                    {
                        BoundBinaryOperatorKind.Addition => left + right,
                        BoundBinaryOperatorKind.Subtraction => left - right,
                        BoundBinaryOperatorKind.Multiplication => left * right,
                        BoundBinaryOperatorKind.Division => left / right,
                        _ => throw new Exception($"Unexpected operator {bin.OperatorKind}")
                    };
                }
                case BoundUnaryExpression unary:
                {
                    var operand = EvaluateExpression(unary.Operand);
                    return unary.OperatorKind switch
                    {
                        BoundUnaryOperatorKind.Negation => -operand,
                        BoundUnaryOperatorKind.Identity => operand,
                        _ => throw new Exception($"Unexpected unary operator [{unary.OperatorKind}]")
                    };
                }
                // number expression
                case BoundLiteralExpression num:
                    return (int)num.Value;
                // parenthesis

                default:
                    throw new Exception($"Unexpected node {root.Kind}");
            }
        }
    }
}
