using System.Threading.Tasks;
using ProjectDaikoku.Interfaces;

namespace ProjectDaikoku.Core
{
    public abstract class CommandBase : ICommand
    {
        public abstract string Name { get; }
        public abstract string Description { get; }

        public virtual string Execute(string[] args) => "Not implemented.";
        public virtual Task<string> ExecuteAsync(string[] args) => Task.FromResult("Not implemented.");
        public virtual string Help() => $"Usage: {Name} [args]";
    }
}
