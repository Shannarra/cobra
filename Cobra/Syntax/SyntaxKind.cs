namespace Cobra.Syntax
{
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
        BinaryOperationExpression,
        ParethesizedExpression
    }
}