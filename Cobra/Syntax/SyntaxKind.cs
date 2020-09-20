namespace Cobra.Syntax
{
    public enum SyntaxKind
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
        ParenthesizedExpression
    }
}