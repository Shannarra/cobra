using System;
using System.Linq;
using Cobra.Syntax;


namespace Cobra
{
    internal class Program
    {
        private static void Main()
        {
            bool print = false;
            while (true)
            {
                Console.Write("cobra >");
                string line = Console.ReadLine();

                if (string.IsNullOrEmpty(line) || line == "exit")
                    return;

                if (line == "printToggle")
                {
                    print = !print;
                    Console.WriteLine($"Printing tree now set to {print}.");
                    continue;
                }
                else if (line == "clear")
                {
                    Console.Clear();
                    continue;
                }

                var tree = SyntaxTree.Parse(line);
                var color = Console.ForegroundColor;
                if (print)
                {
                    

                    Console.ForegroundColor = ConsoleColor.DarkGray;


                    TreePrint(tree.Root);

                    Console.ForegroundColor = color;
                }

                if (tree.Errors.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    foreach (var err in tree.Errors)
                    {
                        Console.WriteLine(err);
                    }

                    Console.ForegroundColor = color;
                }
                else
                {
                    var evaluator = new Evaluator(tree.Root);
                    var res = evaluator.Evaluate();
                    Console.WriteLine(res);
                }
            }
        }

        static void TreePrint(SyntaxNode root, string spacing = "", bool isLast = true) // printing stolen from windows cmd
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
