using System;
using System.Windows.Forms;

namespace ShortcutRunner.HotkeyRegistration
{
    public interface IMessageCatchingWindow : IDisposable
    {
        IntPtr Handle { get; }
        event EventHandler<KeyPressedEventArgs> KeyPressed;
    }

    public class MessageCatchingWindow : NativeWindow, IMessageCatchingWindow
    {
        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        private static int WM_HOTKEY = 0x0312;

        public MessageCatchingWindow()
        {
            CreateHandle(new CreateParams());
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // check if we got a hot key pressed.
            if (m.Msg == WM_HOTKEY)
            {
                // get the keys.
                var key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                var modifier = (ModifierKeys)((int)m.LParam & 0xFFFF);

                // invoke the event to notify the parent.
                if (KeyPressed != null)
                {
                    var shortcutDescription = new ShortcutDescription(modifier, key);
                    KeyPressed(this, new KeyPressedEventArgs(shortcutDescription));
                }
            }
        }

        public void Dispose()
        {
            DestroyHandle();
        }
    }
}