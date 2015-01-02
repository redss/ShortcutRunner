using System;
using System.Text;
using ShortcutRunner.Properties;

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
                    .AppendLine(string.Format(Resources.InvalidConfigurationLineException, LineNumber))
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
                    .AppendLine(string.Format(Resources.InvalidShortcutInConfigurationException, LineNumber))
                    .AppendLine(InvalidLine)
                    .AppendLine()
                    .Append(ShortcutException.Message)
                    .ToString();
            }
        }
    }
}