namespace Cobra.Syntax
{
    /// <summary>
    /// Represents the kind of any <see cref="SyntaxToken"/>
    /// </summary>
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
        LiteralExpression,
        BinaryOperationExpression,
        ParenthesizedExpression
    }
}