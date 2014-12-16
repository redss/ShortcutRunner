using System;

namespace ShortcutRunner
{
    public interface IKeyboardHook : IDisposable
    {
        event EventHandler<KeyPressedEventArgs> KeyPressed;
        void RegisterHotKey(ShortcutDescription shortcutDescription);
    }

    public class KeyboardHook : IKeyboardHook
    {
        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        private readonly IMessageCatchingWindow _messageCatchingWindow;
        private readonly IKeyRegistrationWrapper _keyRegistrationWrapper;

        private int _currentId;

        public KeyboardHook(IMessageCatchingWindow messageCatchingWindow, IKeyRegistrationWrapper keyRegistrationWrapper)
        {
            _messageCatchingWindow = messageCatchingWindow;
            _keyRegistrationWrapper = keyRegistrationWrapper;

            // register the event of the inner native window.
            _messageCatchingWindow.KeyPressed += (sender, args) =>
            {
                if (KeyPressed != null)
                {
                    KeyPressed(this, args);
                }
            };
        }

        public void RegisterHotKey(ShortcutDescription shortcutDescription)
        {
            _currentId = _currentId + 1;
            _keyRegistrationWrapper.RegisterHotKey(_messageCatchingWindow.Handle, _currentId, shortcutDescription);
        }

        public void Dispose()
        {
            for (var i = _currentId; i > 0; i--)
            {
                _keyRegistrationWrapper.UnregisterHotKey(_messageCatchingWindow.Handle, i);
            }

            _messageCatchingWindow.Dispose();
        }
    }
}