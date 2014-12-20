using System;

namespace ShortcutRunner.HotkeyRegistration
{
    public interface IKeyboardHook : IDisposable
    {
        event EventHandler<KeyPressedEventArgs> KeyPressed;
        void RegisterHotKey(ShortcutDescription shortcutDescription);
    }

    public class KeyboardHook : IKeyboardHook
    {
        public event EventHandler<KeyPressedEventArgs> KeyPressed = (sender, args) => { };

        private readonly IMessageCatchingWindow _messageCatchingWindow;
        private readonly IKeyRegistrationController _keyRegistrationController;

        public KeyboardHook(IMessageCatchingWindow messageCatchingWindow, IKeyRegistrationController keyRegistrationController)
        {
            _messageCatchingWindow = messageCatchingWindow;
            _keyRegistrationController = keyRegistrationController;

            _messageCatchingWindow.KeyPressed += OnMessageCatchingWindowOnKeyPressed;
        }

        public void RegisterHotKey(ShortcutDescription shortcutDescription)
        {
            _keyRegistrationController.RegisterHotKey(_messageCatchingWindow.Handle, shortcutDescription);
        }

        public void Dispose()
        {
            _keyRegistrationController.Dispose();
            _messageCatchingWindow.Dispose();
        }

        private void OnMessageCatchingWindowOnKeyPressed(object sender, KeyPressedEventArgs args)
        {
            KeyPressed(this, args);
        }
    }
}