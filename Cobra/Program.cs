using System;
using System.Linq;
using CobraCore.CodeDom;
using CobraCore.CodeDom.Syntax;
using CobraCore.CodeDom.Binding;
using System.Collections.Generic;

namespace Cobra
{
    internal static class Program
    {
        private static void Main()
        {

            var variables = new Dictionary<VariableSymbol, object>();
            var print = false;
            while (true)
            {
                Console.Write("cobra > ");
                var line = Console.ReadLine();

                if (string.IsNullOrEmpty(line) || line == ".exit")
                    return;

                switch (line) // some operation 'commands'
                {
                    case ".printToggle":
                        print = !print;
                        Console.WriteLine($"Printing tree now set to {print}.");
                        continue;
                    case ".clear":
                        Console.Clear();
                        continue;

                    case ".getVars":
                        if (variables.Any())
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Printing contained variable names:");
                            foreach (KeyValuePair<VariableSymbol, object> item in variables)
                                Console.WriteLine($"Variable: {item.Key} => Value: {item.Value}");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray; 
                            Console.WriteLine("No variables exist yet.");
                            Console.ResetColor();
                        }
                        continue;
                }
                 
                var tree = SyntaxTree.Parse(line);
                var compilation = new Compilation(tree);
                var res = compilation.Evaluate(variables);

                var errList = res.Diagnostics;

                if (print)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    TreePrint(tree.Root);
                    Console.ResetColor();
                }

                if (!errList.Any())
                {
                    Console.WriteLine(res.Value);                    
                }
                else
                {
                    foreach (var diagnostic in errList)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(diagnostic);
                        Console.ResetColor();
                        
                        var pre = line.Substring(0, diagnostic.Span.Start);
                        var err = (line[diagnostic.Span.Start..(diagnostic.Span.End)]);
                        var suff = line.Substring(diagnostic.Span.End);

                        Console.Write("    ");
                        Console.Write(pre);

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(err);
                        Console.ResetColor();

                        Console.Write(suff);
                        Console.WriteLine();
                    }
                }
            }
        }

        /// <summary>
        /// Prints the <see cref="SyntaxTree"/> in a structure line the windows cmd 'tree' command
        /// </summary>
        /// <param name="root">The root <see cref="SyntaxNode"/></param>
        /// <param name="spacing">How much initial spacing to add</param>
        /// <param name="isLast">Is the child leaf (last in the tree)?</param>
        private static void TreePrint(SyntaxNode root, string spacing = "", bool isLast = true) // printing stolen from windows cmd
        {
            var symbol = isLast ? "└──" : "├──";
            
            Console.Write($"{spacing}{symbol}{root.Kind}");

            if (root is SyntaxToken token && token.Value != null)
                Console.Write($"\t{token.Value}");

            Console.WriteLine();

            spacing += isLast ? "\t" : "│\t";

            var lastChild = root.GetChildren().LastOrDefault();

            foreach (var child in root.GetChildren())
                TreePrint(child, spacing, child == lastChild);
        }
    }
}
