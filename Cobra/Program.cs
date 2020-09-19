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
            }
        }
    }
}
