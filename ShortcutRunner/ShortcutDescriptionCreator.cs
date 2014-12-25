using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner
{
    public interface IShortcutDescriptionCreator
    {
        ShortcutDescription Parse(string shortcut);
    }

    public class ShortcutParsingException : Exception
    {
    }

    public class NoNonModifierKeysException : ShortcutParsingException
    {
    }

    public class KeyNotRecognizedException : ShortcutParsingException
    {
        public string NotRecognizedKey { get; set; }
    }

    public class MultipleNonModifierKeysException : ShortcutParsingException
    {
    }

    public class ShortcutDescriptionCreator : IShortcutDescriptionCreator
    {
        interface IParsedKey
        {
        }

        class ParsedModifier : IParsedKey
        {
            public ModifierKeys ModifierKeys { get; set; }
        }

        class ParsedKey : IParsedKey
        {
            public Keys Key { get; set; }
        }

        public ShortcutDescription Parse(string shortcut)
        {
            if (shortcut == null)
            {
                throw new ArgumentNullException("shortcut");
            }

            var parts = GetShortcutParts(shortcut)
                .Select(ParseKey)
                .ToArray();

            if (!parts.OfType<ParsedKey>().Any())
            {
                throw new NoNonModifierKeysException();
            }

            if (parts.OfType<ParsedKey>().Count() > 1)
            {
                throw new MultipleNonModifierKeysException();
            }

            var modifierKeys = parts
                .OfType<ParsedModifier>()
                .Select(p => p.ModifierKeys)
                .Aggregate((a, b) => a | b);

            var key = parts
                .OfType<ParsedKey>()
                .Single()
                .Key;

            return new ShortcutDescription(modifierKeys, key);
        }

        private IEnumerable<string> GetShortcutParts(string shortcut)
        {
            return shortcut.Split('+').Select(s => s.Trim());
        }

        private IParsedKey ParseKey(string keyString)
        {
            var modifier = TryParseModifier(keyString);
            var key = TryParseKey(keyString);

            if (modifier.HasValue)
            {
                return new ParsedModifier { ModifierKeys = modifier.Value };
            }
            if (key.HasValue)
            {
                return new ParsedKey { Key = key.Value };
            }

            throw new KeyNotRecognizedException{ NotRecognizedKey = keyString };
        }

        private ModifierKeys? TryParseModifier(string key)
        {
            ModifierKeys result;
            var parsed = Enum.TryParse(key, true, out result);

            return parsed ? (ModifierKeys?) result : null;
        }

        private Keys? TryParseKey(string key)
        {
            Keys parsedKey;
            var keyValid = Enum.TryParse(key, true, out parsedKey);

            return keyValid ? (Keys?) parsedKey : null;
        }
    }
}