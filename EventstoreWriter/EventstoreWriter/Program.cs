using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventstoreWriter
{
    class Program
    {
        static List<string> quotations = new List<string>
                                            {
                                                "Philander Johnson: Cheer up, the worst is yet to come.",
                                                "Honoré de Balzac: Behind every great fortune there is a crime.",
                                                "Tom Wilson, Ziggy: Honesty is the best image.",
                                                "Will Rogers: Common sense ain't common.",
                                                "Mark Twain: When in doubt, tell the truth.",
                                                "Marvin Kitman: A coward is a hero with a wife, kids, and a mortgage.",
                                                "Haythum R. Khalid: All power corrupts, but we need electricity.",
                                                "Gary Kasparov: Chess is mental torture.",
                                                "George Bernard Shaw: Do not try to live for ever. You will not succeed."
                                            };
        static void Main(string[] args)
        {
            for (var i = 0; i < quotations.Count; i++)
            {
                Console.WriteLine("Press {0} to add quotation: {1}", i, quotations.ElementAt(i));
            }
            Console.WriteLine("press q to quit");
            var running = true;
            while (running)
            {
                var input = Console.ReadKey().KeyChar;
                ClearLine();
                if (input == 'q')
                {
                    running = false;
                    continue;
                }
                int number;
                if (Int32.TryParse(input.ToString(), out number))
                {
                    if (number < quotations.Count)
                    {
                        Console.WriteLine("quotation {0} selected", number);
                    }
                    else
                    {
                        Console.WriteLine("Unknown quote - try again!");
                    }
                }
            }
        }

        static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }
}
