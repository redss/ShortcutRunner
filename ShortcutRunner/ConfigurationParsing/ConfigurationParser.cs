using ShortcutRunner.ShortcutDescriptionParsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
            var lines = configurationSource.Split(Environment.NewLine);

            return ParseLines(lines).ToArray();
        }

        private IEnumerable<ConfigurationLine> ParseLines(string[] lines)
        {
            for (var lineNumber = 0; lineNumber < lines.Length; lineNumber++)
            {
                var line = lines[lineNumber];

                if (!string.IsNullOrWhiteSpace(line))
                {
                    yield return ParseLine(line, lineNumber);
                }
            }
        }

        private readonly Regex _lineRegex = new Regex(@"^(?<shortcut>.*?)\s*->\s*(?<command>.*?)$");

        private ConfigurationLine ParseLine(string line, int lineNumber)
        {
            var match = _lineRegex.Match(line);

            if (!match.Success)
            {
                throw new InvalidLineException
                {
                    InvalidLine = line,
                    LineNumber = lineNumber
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