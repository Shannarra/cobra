namespace CobraCore.CodeDom.Syntax
{
    /// <summary>
    /// Represents the kind of any <see cref="SyntaxToken"/>
    /// </summary>
    public enum SyntaxKind
    {
        // Primitive syntax
        Number,
        WhiteSpace,
        EqualsToken,
        BooleanNot,
        Plus,
        Minus,
        Star,
        Slash,
        ParenthesisOpen,
        ParenthesisClose,
        Error,
        EndOfFile,
        BooleanAnd,             // &&
        BooleanOr,              // ||
        TextAndKeyword,         // and
        TextOrKeyword,          // or
        IsTextBooleanKeyword,   // is (keyword)
        NegatedIsTextKeyword,   // !is 

        // Expressions
        LiteralExpression,
        BinaryOperationExpression,
        ParenthesizedExpression,
        UnaryOperationExpression,
        NameExpression,

        // keywords
        FalseKeyword,
        TrueKeyword,
        Identifier,


        NotEquals,
        DoubleEquals,
        AssignmentExpression,
    }
}