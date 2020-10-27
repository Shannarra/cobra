using CobraCore.CodeDom.Syntax;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CobraCore.CodeDom
{
    internal sealed class DiagnosticContainer: IEnumerable<Diagnostic>
    {
        private readonly List<Diagnostic> diagnostics = new List<Diagnostic>();

        private void Report(TextSpan span, string message)
        {
            var diagnostic = new Diagnostic(span, message);
            diagnostics.Add(diagnostic);
        }

        internal void AddRange(DiagnosticContainer diagnostics) 
            => this.diagnostics.AddRange(diagnostics.diagnostics);
        
        public IEnumerator<Diagnostic> GetEnumerator() => diagnostics.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void ReportInvalidNumber(TextSpan textSpan, string text, Type type)
            => Report(textSpan, $"The number {text} is NOT a valid {type}.");

        public void ReportBadChar(int position, char current)
            => Report(new TextSpan(position, 1), $"Bad character \"{current}\".");

        public void ReportUnexpectedToken(TextSpan span, SyntaxKind actual, SyntaxKind expected)
            => Report(span, $"Unexpected token <[{actual}]>, expected <[{expected}]>.");

        public void ReportUndefinedUnaryOperator(TextSpan span, string text, Type type)
            => Report(span, $"Unary operator {text} is not defined for type {type}");

        internal void ReportUndefinedBinaryOperator(TextSpan span, string text, Type left, Type right)
            => Report(span, $"Binary operator {text} is not defined for types {left} and {right}");
    }
}
