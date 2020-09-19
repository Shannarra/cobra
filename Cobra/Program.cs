using System;
using System.Linq;

namespace Cobra
{
    internal class Program
    {
        private static void Main()
        {
            while (true)
            {
                Console.Write("cobra >");
                string line = Console.ReadLine();

                if (string.IsNullOrEmpty(line))
                    return;

                var parser = new Parser(line);
                var tree = parser.Parse();
                var color = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.DarkGray;

                TreePrint(tree.Root);
                  
                Console.ForegroundColor = color;
                if (parser.Errors.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    foreach (var err in parser.Errors)
                    {
                        Console.WriteLine(err);
                    }

                    Console.ForegroundColor = color;
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
