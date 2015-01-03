using System;
using System.Linq;
using System.Windows.Forms;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner.ShortcutParsing
{
    public interface IKeyParser
    {
        IKeyToken Parse(string key);
    }

    public class KeyParser : IKeyParser
    {
        public IKeyToken Parse(string keyString)
        {
            if (IsDefinedInEnum<ModifierKeys>(keyString))
            {
                var modifierKey = ParseEnum<ModifierKeys>(keyString);
                
                return new ModifierKeyToken(modifierKey);
            }

            if (IsDefinedInEnum<Keys>(keyString))
            {
                var key = ParseEnum<Keys>(keyString);

                return new KeyToken(key);
            }

            throw new KeyNotRecognizedException
            {
                NotRecognizedKey = keyString
            };
        }

        private bool IsDefinedInEnum<TEnum>(string name)
        {
            return Enum.GetNames(typeof (TEnum))
                .Any(n => n.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        private TEnum ParseEnum<TEnum>(string name)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), name, true);
        }
    }
}