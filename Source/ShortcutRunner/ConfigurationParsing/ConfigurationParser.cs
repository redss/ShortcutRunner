using ShortcutRunner.HotkeyRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ShortcutRunner.ShortcutParsing;

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
                throw new InvalidConfigurationLineException
                {
                    InvalidLine = line,
                    LineNumber = lineNumber + 1
                };
            }

            return new ConfigurationLine
            {
                Shortcut = ParseShortcut(match.Groups["shortcut"].Value, line, lineNumber),
                Command = match.Groups["command"].Value
            };
        }

        private ShortcutDescription ParseShortcut(string shortcut, string line, int lineNumber)
        {
            try
            {
                return ShortcutParser.Create(shortcut);
            }
            catch (Exception e)
            {
                throw new InvalidShortcutInConfigurationException
                {
                    InvalidLine = line,
                    LineNumber = lineNumber + 1,
                    ShortcutException = e
                };
            }
        }
    }
}