using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner
{
    public interface IShortcutDescriptionParser
    {
        IEnumerable<IKeyToken> Parse(string shortcut);
    }

    public class ShortcutDescriptionParser : IShortcutDescriptionParser
    {
        public IEnumerable<IKeyToken> Parse(string shortcut)
        {
            return GetShortcutParts(shortcut)
                .Select(ParseKey)
                .ToArray();
        }

        private IEnumerable<string> GetShortcutParts(string shortcut)
        {
            return shortcut.Split('+').Select(s => s.Trim());
        }

        private IKeyToken ParseKey(string keyString)
        {
            var modifier = TryParseModifier(keyString);
            var key = TryParseKey(keyString);

            if (modifier.HasValue)
            {
                return new ModifierKeyToken(modifier.Value);
            }
            if (key.HasValue)
            {
                return new KeyToken(key.Value);
            }

            throw new KeyNotRecognizedException { NotRecognizedKey = keyString };
        }

        private ModifierKeys? TryParseModifier(string key)
        {
            ModifierKeys result;
            var parsed = Enum.TryParse(key, true, out result);

            return parsed ? (ModifierKeys?)result : null;
        }

        private Keys? TryParseKey(string key)
        {
            Keys parsedKey;
            var keyValid = Enum.TryParse(key, true, out parsedKey);

            return keyValid ? (Keys?)parsedKey : null;
        }
    }
}