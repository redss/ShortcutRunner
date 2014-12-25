using System;
using System.Windows.Forms;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner
{
    public interface IKeyParser
    {
        IKeyToken Parse(string key);
    }

    public class KeyParser : IKeyParser
    {
        public IKeyToken Parse(string keyString)
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