using ProjectDaikoku.Modules;
using ProjectDaikoku.Interfaces;
using ProjectDaikoku.Core;

namespace ProjectDaikoku
{
    internal class Program
    {

        static void Main(string[] args)
        {
            var handler = new CommandHandler();

            Console.WriteLine("Type 'help' to list commands. Type 'exit' to quit.");

            while (true)
            {
                Console.Write("Daikoku >> ");
                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;

                if (input.Equals("help", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(handler.Help());
                    continue;
                }

                var output = handler.Execute(input);
                Console.WriteLine(output);
            }
        }
    }
}
