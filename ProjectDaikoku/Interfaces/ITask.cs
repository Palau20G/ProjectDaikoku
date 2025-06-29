// C2-Like Tasks queue or background jobs

namespace ProjectDaikoku.Intefaces
{
    public interface ITask
    {
        string Id { get; }
        string Status { get; }
        void Run();
    }

}