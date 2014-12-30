using System;

namespace ShortcutRunner.ConfigurationParsing
{
    public class InvalidLineException : Exception
    {
        public string InvalidLine { get; set; }
        public int LineNumber { get; set; }
    }
}