namespace ProjectDaikoku.Interfaces
{
    public interface ICommand
    {
        string Name { get; }

        string Description { get; }

        string Execute(string[] args); // For Sync Usage

        Task<string> ExecuteAsync(string[] args); // For Async Usage

        string Help(); // Optional help/usage string
        
    }
}