using System;
using System.Linq;
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
            var parts = line.Split("->");

            // todo: check validity

            return new ConfigurationLine
            {
                Shortcut = ShortcutDescriptionCreator.Create(parts[0]),
                Command = parts[1]
            };
        }
    }

    static class StringExtensions
    {
        public static string[] Split(this string str, string separator)
        {
            return str.Split(new[] {separator}, StringSplitOptions.None);
        }
    }
}