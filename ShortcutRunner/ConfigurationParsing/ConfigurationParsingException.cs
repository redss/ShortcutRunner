using System;
using System.Text;

namespace ShortcutRunner.ConfigurationParsing
{
    public abstract class ConfigurationParsingException : Exception
    {
        public int LineNumber { get; set; }
        public string InvalidLine { get; set; }
    }

    public class InvalidConfigurationLineException : ConfigurationParsingException
    {
        public override string Message
        {
            get
            {
                return new StringBuilder()
                    .AppendLine(string.Format("Line {0} does not match shortcut description format:", LineNumber))
                    .Append(InvalidLine)
                    .ToString();
            }
        }
    }

    public class InvalidShortcutInConfigurationException : ConfigurationParsingException
    {
        public Exception ShortcutException { get; set; }

        public override string Message
        {
            get
            {
                return new StringBuilder()
                    .AppendLine(string.Format("Shortcut at line {0} is invalid:", LineNumber))
                    .AppendLine(InvalidLine)
                    .AppendLine()
                    .Append(ShortcutException.Message)
                    .ToString();
            }
        }
    }
}