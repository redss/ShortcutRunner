﻿using System.Windows.Forms;

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

        public static ShortcutDescription Ctrl(Keys key)
        {
            return new ShortcutDescription(ModifierKeys.Ctrl, key);
        }

        public static ShortcutDescription Alt(Keys key)
        {
            return new ShortcutDescription(ModifierKeys.Ctrl, key);
        }

        public static ShortcutDescription Shift(Keys key)
        {
            return new ShortcutDescription(ModifierKeys.Shift, key);
        }

        public override int GetHashCode()
        {
            // Code generated by resharper.

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