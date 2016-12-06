using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandLine.Model
{
    public class CommandDescriptor
    {
        public CommandDescriptor(
            Type type,
            string command,
            ActionDescriptor defaultAction,
            IEnumerable<ActionDescriptor> actionList,
            string description)
        {
            Type = type;
            DefaultAction = defaultAction;
            Description = description;
            Actions = actionList.ToDictionary(a => a.Name);
            Command = command;
            HasDefaultAction = defaultAction != null;
        }

        public string Command { get; }

        public Type Type { get; }

        public IReadOnlyDictionary<string, ActionDescriptor> Actions { get; }

        public ActionDescriptor DefaultAction { get; }

        public bool HasDefaultAction { get; }

        public string Description { get; }
    }
}