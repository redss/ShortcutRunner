using System.Windows.Forms;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner
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

        public override bool Equals(object obj)
        {
            var modifierToken = obj as ModifierKeyToken;

            return modifierToken != null && modifierToken.ModifierKeys == ModifierKeys;
        }
    }

    public class KeyToken : IKeyToken
    {
        public readonly Keys Key;

        public KeyToken(Keys key)
        {
            Key = key;
        }

        public override bool Equals(object obj)
        {
            var keyToken = obj as KeyToken;

            return keyToken != null && keyToken.Key == Key;
        }
    }
}