using System.Windows.Forms;

namespace ShortcutRunner.HotkeyRegistration
{
    public class ShortcutDescription
    {
        public Keys Key { get; set; }
        public ModifierKeys Modifiers { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Key * 397) ^ (int) Modifiers;
            }
        }

        public override bool Equals(object obj)
        {
            var otherShortcutDescrption = obj as ShortcutDescription;

            return otherShortcutDescrption != null && Equals(otherShortcutDescrption);
        }

        private bool Equals(ShortcutDescription other)
        {
            return Key == other.Key && Modifiers == other.Modifiers;
        }
    }
}