using ProjectDaikoku.Core;
using ProjectDaikoku.Interfaces;
using System;
using System.Linq;

namespace ProjectDaikoku.Modules
{
    public class ManCommand : CommandBase
    {
        public override string Name => "man";
        public override string Description => "Displays usage info for a specific command.";

        public override string Execute(string[] args)
        {
            if (args.Length == 0)
                return "Usage: man <command>";

            string commandName = args[0].ToLower();
            var commandHandler = new CommandHandler();

            var target = commandHandler.GetAllCommands()
                .FirstOrDefault(cmd => cmd.Name.Equals(commandName, StringComparison.OrdinalIgnoreCase));

            return target != null ? target.Help() : $"No manual entry for: {commandName}";
        }
    }
}
