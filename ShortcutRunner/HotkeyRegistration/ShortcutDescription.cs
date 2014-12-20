using System.Windows.Forms;

namespace ShortcutRunner.HotkeyRegistration
{
    public class ShortcutDescription
    {
        public Keys Key { get; private set; }
        public ModifierKeys Modifiers { get; private set; }

        public ShortcutDescription(ModifierKeys modifiers, Keys key)
        {
            Modifiers = modifiers;
            Key = key;
        }

        public static ShortcutDescription Empty
        {
            get
            {
                return new ShortcutDescription(ModifierKeys.None, Keys.None);
            }
        }

        public static ShortcutDescription Shift(Keys key)
        {
            return new ShortcutDescription(ModifierKeys.Shift, key);
        }

        public static ShortcutDescription Ctrl(Keys key)
        {
            return new ShortcutDescription(ModifierKeys.Ctrl, key);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Key * 397) ^ (int) Modifiers;
            }
        }

        public override bool Equals(object obj)
        {
            var otherShortcut = obj as ShortcutDescription;

            return otherShortcut != null 
                && Key == otherShortcut.Key 
                && Modifiers == otherShortcut.Modifiers;
        }
    }
}