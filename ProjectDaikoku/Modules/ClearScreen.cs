
using System;
using ProjectDaikoku.Core;

namespace ProjectDaikoku.Modules
{
    public class ClearScreen : CommandBase
    {
        public override string Name => "cls";
        public override string Description => "clears the terminal";

        public override string Execute(string[] args)
        {
            Console.Clear();
            return "";
        }
    }

}