using System;
using CommandLine.Attributes;

namespace TestCliApp.Commands
{
    [Command]
    public class SourcesCommand
    {
        [Action]
        public void List()
        {

        }

        [Action]
        public void Add(string name, Uri uri)
        {

        }

        [Action]
        public void Delete(string name)
        {

        }

        [Action]
        public void Update(string name, Uri uri)
        {

        }
    }

}