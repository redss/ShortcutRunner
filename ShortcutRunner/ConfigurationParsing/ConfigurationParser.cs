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
        public readonly IShortcutParser ShortcutParser;

        public ConfigurationParser(IShortcutParser shortcutParser)
        {
            ShortcutParser = shortcutParser;
        }

        public ConfigurationLine[] Parse(string configurationSource)
        {
            var lines = SplitToLines(configurationSource);

            return ParseLines(lines).ToArray();
        }

        private string[] SplitToLines(string str)
        {
            return str.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }

        private IEnumerable<ConfigurationLine> ParseLines(string[] lines)
        {
            for (var lineNumber = 0; lineNumber < lines.Length; lineNumber++)
            {
                var line = lines[lineNumber];

                if (LineIsNotCommentNorEmpty(line))
                {
                    yield return ParseLine(line, lineNumber);
                }
            }
        }

        private readonly Regex _emptyRegex = new Regex(@"^\s*$");
        private readonly Regex _commentRegex = new Regex(@"^\s*#.*$");

        private bool LineIsNotCommentNorEmpty(string line)
        {
            return !_emptyRegex.IsMatch(line) && !_commentRegex.IsMatch(line);
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
                Shortcut = ShortcutParser.Create(match.Groups["shortcut"].Value),
                Command = match.Groups["command"].Value
            };
        }
    }
}