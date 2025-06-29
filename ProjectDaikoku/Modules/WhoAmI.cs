
using System;
using ProjectDaikoku.Core;

namespace ProjectDaikoku.Modules
{
    public class WhoAmICommand : CommandBase
    {
        public override string Name => "whoami";
        public override string Description => "Displays the current user.";

        public override string Execute(string[] args)
        {
            return Environment.UserName;
        }
    }

}