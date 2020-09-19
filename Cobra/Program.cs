using System;

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

                var lexer = new Lexer(line);

                while (true)
                {
                    var next = lexer.NextToken();

                    if (next.Kind == SyntaxKind.EndOfFile)
                        break;

                    Console.WriteLine($"{next.Kind} => {next.Text}");

                    if (next.Value != null)
                        Console.WriteLine($"{next.Value}");
                }
            }
        }
    }
}
