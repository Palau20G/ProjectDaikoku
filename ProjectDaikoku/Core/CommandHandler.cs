using ProjectDaikoku.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

public class CommandHandler
{
    private readonly Dictionary<string, ICommand> commands = new();

    public CommandHandler()
    {
        LoadCommands();
    }

    private void LoadCommands()
    {
        var commandTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t =>
                typeof(ICommand).IsAssignableFrom(t) &&
                !t.IsInterface &&
                !t.IsAbstract);

        foreach (var type in commandTypes)
        {
            if (Activator.CreateInstance(type) is ICommand cmd)
            {
                var key = cmd.Name?.ToLower();
                if (!string.IsNullOrWhiteSpace(key) && !commands.ContainsKey(key))
                {
                    commands[key] = cmd;
                }
            }
        }
    }


    public string Execute(string input)
    {
        var parts = input.Split(' ', 2);
        var name = parts[0].ToLower();
        var args = parts.Length > 1 ? parts[1].Split(' ') : Array.Empty<string>();

        if (commands.TryGetValue(name, out var command))
        {
            return command.Execute(args);
        }

        return $"Unknown command: {name}";
    }

    public string Help()
    {
        var output = new StringBuilder("Available commands:\n");
        foreach (var cmd in commands.Values)
        {
            string mode;
            try
            {
                cmd.Execute(Array.Empty<string>());
                mode = "sync";
            }
            catch (NotImplementedException)
            {
                try
                {
                    cmd.ExecuteAsync(Array.Empty<string>()).Wait();
                    mode = "async";
                }
                catch
                {
                    mode = "unknown";
                }
            }

            output.AppendLine($"{cmd.Name.PadRight(18)} - {cmd.Description} ({mode})");
        }
        return output.ToString();
    }

    public IEnumerable<ICommand> GetAllCommands()
    {
        return commands.Values;
    }

}
