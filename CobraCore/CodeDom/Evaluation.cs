using System;
using System.Collections;
using System.Collections.Generic;
using CobraCore.CodeDom.Binding;

namespace CobraCore.CodeDom
{
    /// <summary>
    /// Evaluator for expressions
    /// </summary>
    internal class Evaluator
    {
        private Dictionary<string, object> variables;

        /// <summary>
        /// Root expression to evaluate
        /// </summary>
        public BoundExpression Root { get; }

        public Evaluator(BoundExpression root)
        {
            Root = root;
        }

        public Evaluator(BoundExpression root, Dictionary<string, object> variables) : this(root)
        {
            this.variables = variables;
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
                // number expression
                case BoundLiteralExpression num:
                    return num.Value;

                case BoundVariableExpression v:
                    return variables[v.Name];

                case BoundAssignmentExpression ass:
                {
                    var val = EvaluateExpression(ass.Expression);
                    variables[ass.Name] = val;
                    return val;
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
                        BoundBinaryOperatorKind.Equals => Equals(left, right),
                        BoundBinaryOperatorKind.NotEquals => !Equals(left, right),
                        _ => throw new Exception($"Unexpected operator {bin.Operator.Kind}")
                    };
                }

                default:
                    throw new Exception($"Unexpected node {root.Kind}");
            }
        }
    }
}
