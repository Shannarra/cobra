using System;
using System.Linq;
using CobraCore.CodeDom;
using CobraCore.CodeDom.Syntax;
using CobraCore.CodeDom.Binding; 


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

                switch (line) // some operation 'commands'
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
                var compilation = new Compilation(tree);
                var res = compilation.Evaluate();

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
                    Console.ForegroundColor = ConsoleColor.Red;
                    foreach (var err in errList)
                        Console.WriteLine(err);
                    Console.ResetColor();
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
