using System;
using System.Linq;
using System.Windows.Forms;

namespace ShortcutRunner
{
    public class ShortcutDescription
    {
        public Keys Key { get; set; }
        public ModifierKeys Modifiers { get; set; }
    }

    public interface IShortcutDescriptionParser
    {
        ShortcutDescription Parse(string shortcut);
    }

    public class ShortcutDescriptionParser : IShortcutDescriptionParser
    {
        public ShortcutDescription Parse(string shortcut)
        {
            if (shortcut == null)
            {
                throw new ArgumentNullException("shortcut");
            }

            var parts = shortcut.Split('+').Select(s => s.Trim()).ToArray();

            var result = new ShortcutDescription
            {
                Modifiers = 0,
                Key = Keys.None
            };

            foreach (var part in parts)
            {
                var modifier = TryParseModifier(part);

                if (modifier.HasValue)
                {
                    result.Modifiers |= modifier.Value;
                    continue;
                }

                var key = TryParseKey(part);

                if (key.HasValue && result.Key != Keys.None)
                {
                    throw new ArgumentException("Shortcut description is invalid: more then one non-modifier key is used.", "shortcut");
                }

                if (key.HasValue)
                {
                    result.Key = key.Value;
                    continue;
                }

                throw new ArgumentException("Shortcut description is invalid.", "shortcut");
            }

            return result;
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