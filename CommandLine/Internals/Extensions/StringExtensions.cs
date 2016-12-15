namespace CommandLine.Internals.Extensions
{
    public static class StringExtensions
    {
        public static string DefaultIfWhitespace(this string s, string defaultAlternative)
        {
            return string.IsNullOrWhiteSpace(s) ? defaultAlternative : s;
        }
    }
}