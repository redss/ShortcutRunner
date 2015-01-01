using System;

namespace ShortcutRunner.ConfigurationParsing
{
    public class InvalidLineException : Exception
    {
        public string InvalidLine { get; set; }
        public int LineNumber { get; set; }

        public InvalidLineException()
        {
        }

        public InvalidLineException(Exception innerException) : base("", innerException)
        {
        }
    }
}