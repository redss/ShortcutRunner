using System.Windows.Forms;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner.ShortcutParsing
{
    public interface IKeyToken
    {
    }

    public class ModifierKeyToken : IKeyToken
    {
        public readonly ModifierKeys ModifierKeys;

        public ModifierKeyToken(ModifierKeys modifierKeys)
        {
            ModifierKeys = modifierKeys;
        }
    }

    public class KeyToken : IKeyToken
    {
        public readonly Keys Key;

        public KeyToken(Keys key)
        {
            Key = key;
        }
    }
}