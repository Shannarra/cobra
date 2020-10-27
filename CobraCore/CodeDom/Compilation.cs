using System;
using System.Linq;
using CobraCore.CodeDom.Binding;
using CobraCore.CodeDom.Syntax;

namespace CobraCore.CodeDom
{
    public class Compilation
    {
        public Compilation(SyntaxTree syntax)
        {
            Syntax = syntax;
        }

        public SyntaxTree Syntax { get; }
    
        public EvaluationResult Evaluate()
        {
            var binder = new Binder();
            var expr = binder.Bind(Syntax.Root);

            var errs = Syntax.Diagnostics.Concat(binder.Diagnostics).ToArray();

            if (errs.Any())
                return new EvaluationResult(errs, null);


            var evaluator = new Evaluator(expr);
            var val = evaluator.Evaluate();

            return new EvaluationResult(Array.Empty<Diagnostic>(), val);
        }
    }
}
