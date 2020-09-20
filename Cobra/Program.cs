using System;
using System.Linq;
using Cobra.Syntax;


namespace Cobra
{
    internal static class Program
    {
        private static void Main()
        {
            var print = false;
            while (true)
            {
                Console.Write("cobra >");
                var line = Console.ReadLine();

                if (string.IsNullOrEmpty(line) || line == "exit")
                    return;

                switch (line) //some operation 'commands'
                {
                    case "printToggle":
                        print = !print;
                        Console.WriteLine($"Printing tree now set to {print}.");
                        continue;
                    case "clear":
                        Console.Clear();
                        continue;
                }

                var tree = SyntaxTree.Parse(line);
                if (print)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    TreePrint(tree.Root);
                    Console.ResetColor();
                }

                if (tree.Errors.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    foreach (var err in tree.Errors)
                        Console.WriteLine(err);
                    Console.ResetColor();
                }
                else
                {
                    var evaluator = new Evaluator(tree.Root);
                    var res = evaluator.Evaluate();
                    Console.WriteLine(res);
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
