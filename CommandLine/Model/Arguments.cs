using System.Collections.Generic;

namespace CommandLine.Model
{
    public class Arguments : Dictionary<string, string>
    {
        public string Action { get; }

        public Arguments(string action)
        {
            Action = action;
        }

        public Arguments()
        {
            Action = null;
        }
    }
}