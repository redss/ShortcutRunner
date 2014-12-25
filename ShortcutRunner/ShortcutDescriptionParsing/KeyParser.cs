using System;
using System.Windows.Forms;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner.ShortcutDescriptionParsing
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
            return Enum.IsDefined(typeof (TEnum), name);
        }

        private TEnum ParseEnum<TEnum>(string name)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), name);
        }
    }
}