﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using CommandLine.Attributes;
using CommandLine.Extensions;
using CommandLine.Model;

namespace CommandLine.Factories
{
    internal class CommandDescriptorFactory
    {
        public static CommandDescriptor CreateFor<T>()
            where T : class
        {
            var type = typeof(T);
            return CreateFor(type);
        }

        public static CommandDescriptor CreateFor(Type type)
        {
            var commandAttribute = type.GetCustomAttribute<CommandAttribute>();
            if (commandAttribute == null)
                throw new InvalidOperationException();

            // determine command name
            var commandName = commandAttribute.Name?.ToLower();
            if (string.IsNullOrEmpty(commandName))
            {
                commandName = type.Name.ToLower();
                if (commandName.EndsWith("command"))
                    commandName = commandName.Substring(0, commandName.Length - "command".Length);
            }

            // identify any actions
            var methodList = type.GetMethods();
            ActionDescriptor defaultAction = null;

            if (!methodList.Any(x => x.HasCustomAttribute<ActionAttribute>()))
            {
                throw new InvalidOperationException();
            }

            var actionList = methodList.Where(m => m.HasCustomAttribute<ActionAttribute>())
                .Select(ActionDescriptorFactory.CreateFor)
                .ToList();

            // find the default action
            if (actionList.Count == 1)
            {
                defaultAction = actionList.First();
                actionList.Clear();
            }
            else
            {
                if (actionList.Count(descriptor => descriptor.IsDefault) > 1)
                    throw new InvalidOperationException("Only one action can be tagged as default.");
                defaultAction = actionList.FirstOrDefault(x => x.IsDefault);
                if (defaultAction != null)
                    actionList.Remove(defaultAction);
            }

            // fetch description
            var descriptionAttribute = type.GetCustomAttribute<DescriptionAttribute>();

            return new CommandDescriptor(type,
                commandName,
                defaultAction,
                actionList,
                descriptionAttribute?.Description);
        }
    }
}