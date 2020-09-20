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
        public object Evaluate()
        {
            return EvaluateExpression(Root);
        }

        /// <summary>
        /// Evaluates the <paramref name="root"/>
        /// </summary>
        /// <param name="root">The root to start evaluation from</param>
        /// <returns></returns>
        private object EvaluateExpression(BoundExpression root)
        {
            switch (root)
            {
                // binary expression
                case BoundBinaryExpression bin:
                {
                    var left = EvaluateExpression(bin.Left);
                    var right = EvaluateExpression(bin.Right);

                    return bin.Operator.Kind switch
                    {
                        BoundBinaryOperatorKind.Addition => (int)left + (int)right,
                        BoundBinaryOperatorKind.Subtraction => (int)left - (int)right,
                        BoundBinaryOperatorKind.Multiplication => (int)left * (int)right,
                        BoundBinaryOperatorKind.Division => (int)left / (int)right,
                        BoundBinaryOperatorKind.LogicalAnd => (bool)left && (bool)right,
                        BoundBinaryOperatorKind.LogicalOr => (bool)left || (bool)right,
                        _ => throw new Exception($"Unexpected operator {bin.Operator.Kind}")
                    };
                }
                case BoundUnaryExpression unary:
                {
                    var operand = EvaluateExpression(unary.Operand);
                    return unary.OperatorKind switch
                    {
                        BoundUnaryOperatorKind.Negation => -(int)operand,
                        BoundUnaryOperatorKind.Identity => (int)operand,
                        BoundUnaryOperatorKind.LogicalNegation => !(bool)operand,
                        _ => throw new Exception($"Unexpected unary operator [{unary.OperatorKind}]")
                    };
                }
                // number expression
                case BoundLiteralExpression num:
                    return num.Value;
                // parenthesis

                default:
                    throw new Exception($"Unexpected node {root.Kind}");
            }
        }
    }
}
