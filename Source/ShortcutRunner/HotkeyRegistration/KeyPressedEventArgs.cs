using System;

namespace ShortcutRunner.HotkeyRegistration
{
    /// <summary>
    /// Event Args for the event that is fired after the hot key has been pressed.
    /// </summary>
    public class KeyPressedEventArgs : EventArgs
    {
        public ShortcutDescription ShortcutDescription { get; private set; }

        public KeyPressedEventArgs(ShortcutDescription shortcutDescription)
        {
            ShortcutDescription = shortcutDescription;
        }
    }
}