using System;
using System.Linq;
using System.Text.RegularExpressions;
using ShortcutRunner.ShortcutDescriptionParsing;

namespace ShortcutRunner.ConfigurationParsing
{
    public interface IConfigurationParser
    {
        ConfigurationLine[] Parse(string configurationSource);
    }

    public class ConfigurationParser : IConfigurationParser
    {
        public readonly IShortcutDescriptionCreator ShortcutDescriptionCreator;

        public ConfigurationParser(IShortcutDescriptionCreator shortcutDescriptionCreator)
        {
            ShortcutDescriptionCreator = shortcutDescriptionCreator;
        }

        public ConfigurationLine[] Parse(string configurationSource)
        {
            return configurationSource
                .Split(Environment.NewLine)
                .Where(IsNotEmpty)
                .Select(ParseLine)
                .ToArray();
        }

        private bool IsNotEmpty(string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        private ConfigurationLine ParseLine(string line)
        {
            var regex = new Regex(@"^(?<shortcut>.*?)\s*->\s*(?<command>.*?)$");

            var match = regex.Match(line);

            if (!match.Success)
            {
                throw new InvalidLineException
                {
                    InvalidLine = line
                };
            }

            return new ConfigurationLine
            {
                Shortcut = ShortcutDescriptionCreator.Create(match.Groups["shortcut"].Value),
                Command = match.Groups["command"].Value
            };
        }
    }

    public class InvalidLineException : Exception
    {
        public string InvalidLine { get; set; }
    }

    static class StringExtensions
    {
        public static string[] Split(this string str, string separator)
        {
            return str.Split(new[] {separator}, StringSplitOptions.None); // todo: test, null check
        }
    }
}