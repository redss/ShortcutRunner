using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ShortcutRunner
{
    public interface IKeyboardHook : IDisposable
    {
        event EventHandler<KeyPressedEventArgs> KeyPressed;
        void RegisterHotKey(ShortcutDescription shortcutDescription);
    }

    public class KeyboardHook : IKeyboardHook
    {
        // Registers a hot key with Windows.

        private readonly Window _window = new Window();
        private int _currentId;

        public KeyboardHook()
        {
            // register the event of the inner native window.
            _window.KeyPressed += delegate(object sender, KeyPressedEventArgs args)
            {
                if (KeyPressed != null)
                {
                    KeyPressed(this, args);
                }
            };
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public void RegisterHotKey(ShortcutDescription shortcutDescription)
        {
            _currentId = _currentId + 1;

            if (!RegisterHotKey(_window.Handle, _currentId, (uint) shortcutDescription.Modifiers, (uint) shortcutDescription.Key))
            {
                throw new InvalidOperationException("Couldn’t register the hot key.");
            }
        }

        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        public void Dispose()
        {
            for (var i = _currentId; i > 0; i--)
            {
                UnregisterHotKey(_window.Handle, i);
            }

            _window.Dispose();
        }
        
        /// <summary>
        /// Represents the window that is used internally to get the messages.
        /// </summary>
        private class Window : NativeWindow, IDisposable
        {
            private static int WM_HOTKEY = 0x0312;

            public Window()
            {
                CreateHandle(new CreateParams());
            }

            /// <summary>
            /// Overridden to get the notifications.
            /// </summary>
            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                // check if we got a hot key pressed.
                if (m.Msg == WM_HOTKEY)
                {
                    // get the keys.
                    var key = (Keys) (((int) m.LParam >> 16) & 0xFFFF);
                    var modifier = (ModifierKeys) ((int) m.LParam & 0xFFFF);

                    // invoke the event to notify the parent.
                    if (KeyPressed != null)
                    {
                        var shortcutDescription = new ShortcutDescription
                        {
                            Modifiers = modifier,
                            Key = key
                        };

                        KeyPressed(this, new KeyPressedEventArgs(shortcutDescription));
                    }
                }
            }

            public event EventHandler<KeyPressedEventArgs> KeyPressed;

            public void Dispose()
            {
                DestroyHandle();
            }
        }
    }
}