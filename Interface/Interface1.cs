using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDaikoku.Interface
{
    public interface ICommand
    {
        string Name { get; }
        string Description { get; }
        string Execute(string[] args);
        Task<string> ExecuteAsync(string[] args);

        string Help();
    }
}
