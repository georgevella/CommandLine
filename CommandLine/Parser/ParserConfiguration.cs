namespace CommandLine.Parser
{
    public class ParserConfiguration
    {
        public string ShortNameSwitchTag { get; set; }

        public string NameSwitchTag { get; set; }

        public ParserConfiguration()
        {
            ShortNameSwitchTag = "-";
            NameSwitchTag = "--";
        }
    }
}