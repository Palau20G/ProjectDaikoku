//Larger modules that need lifecycle hooks
namespace ProjectDaikoku.Intefaces
{
    public interface IModule
    {
        string Name { get; }
        string Description { get; }
        void Start();   // Activate or install
        void Stop();    // Cleanup or disable
    }
}