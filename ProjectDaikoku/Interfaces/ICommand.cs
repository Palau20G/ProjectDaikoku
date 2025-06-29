namespace ProjectDaikoku.Intefaces
{
    public class ICommand
    {
        string Name { get; }
        string Description { get; }
        string Execute(string[] args);

        string Help();
    }
}