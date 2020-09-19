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
                var expr = parser.Parse();
                var color = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.DarkGray;

                TreePrint(expr);
                  
                Console.ForegroundColor = color;
            }
        }

        static void TreePrint(SyntaxNode node, string spacing = "", bool isLast = true) // printing stolen from windows cmd
        {
            var symbol = isLast ? "└──" : "├──";
            
            Console.Write($"{spacing}{symbol}{node.Kind}");

            if (node is SyntaxToken token && token.Value != null)
                Console.Write($"\t{token.Value}");

            Console.WriteLine();

            spacing += isLast ? "\t" : "│\t";

            var lastChild = node.GetChildren().LastOrDefault();

            foreach (var child in node.GetChildren())
                TreePrint(child, spacing, child == lastChild);
        }
    }
}
