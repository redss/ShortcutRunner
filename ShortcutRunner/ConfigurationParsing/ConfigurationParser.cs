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
                .Select(WithLineNumbers)
                .Where(IsNotEmpty)
                .Select(ParseLine)
                .ToArray();
        }

        class Line
        {
            public string Text { get; set; }
            public int LineNumber { get; set; }
        }

        private Line WithLineNumbers(string line, int lineNumber)
        {
            return new Line
            {
                LineNumber = lineNumber,
                Text = line
            };
        }

        private bool IsNotEmpty(Line line)
        {
            return !string.IsNullOrWhiteSpace(line.Text);
        }

        private ConfigurationLine ParseLine(Line line)
        {
            var regex = new Regex(@"^(?<shortcut>.*?)\s*->\s*(?<command>.*?)$");

            var match = regex.Match(line.Text);

            if (!match.Success)
            {
                throw new InvalidLineException
                {
                    InvalidLine = line.Text,
                    LineNumber = line.LineNumber
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
        public int LineNumber { get; set; }
    }

    static class StringExtensions
    {
        public static string[] Split(this string str, string separator)
        {
            return str.Split(new[] {separator}, StringSplitOptions.None); // todo: test, null check
        }
    }
}