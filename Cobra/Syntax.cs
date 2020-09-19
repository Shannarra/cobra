using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cobra
{
    class SyntaxToken : SyntaxNode
    {
        public override SyntaxKind Kind { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
            => Enumerable.Empty<SyntaxNode>();

        public int Position { get; }
        public string Text { get; }
        public object Value { get; }

        public SyntaxToken(SyntaxKind kind, int position, string text, object value)
        {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }
    }

    internal enum SyntaxKind
    {
        // Primitive syntax
        Number,
        WhiteSpace,
        Plus,
        Minus,
        Star,
        Slash,
        ParenthesisOpen,
        ParenthesisClose,
        Error,
        EndOfFile,

        // Expressions
        NumberExpression,
        BinaryOperationExpression
    }

    abstract class SyntaxNode
    {
        public abstract SyntaxKind Kind { get; }

        public abstract IEnumerable<SyntaxNode> GetChildren();
    }

    abstract class Expression : SyntaxNode {}

    sealed class NumberExpressionSyntax : Expression
    {
        public NumberExpressionSyntax(SyntaxToken token)
        {
            NumberToken = token;
        }

        public override SyntaxKind Kind => SyntaxKind.NumberExpression;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return NumberToken;
        }

        public SyntaxToken NumberToken { get; }
    }

    sealed class BinaryOperationExpressionSyntax : Expression
    {
        public Expression Left { get; }
        public SyntaxToken OperatorToken { get; }
        public Expression Right { get; }
        public override SyntaxKind Kind => SyntaxKind.BinaryOperationExpression;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Left;
            yield return OperatorToken;
            yield return Right;
        }

        public BinaryOperationExpressionSyntax(Expression left, SyntaxToken operatorToken, Expression right)
        {
            Left = left;
            OperatorToken = operatorToken;
            Right = right;
        }
    }

    class SyntaxTree
    {
        public IReadOnlyList<string> Errors { get; }
        public Expression Root { get; }
        public SyntaxToken EofToken { get; }

        public SyntaxTree(IReadOnlyList<string> errors, Expression root, SyntaxToken eofToken)
        {
            Errors = errors.ToArray();
            Root = root;
            EofToken = eofToken;
        }
    }
}
