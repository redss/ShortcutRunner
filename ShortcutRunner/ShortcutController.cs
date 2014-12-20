using System;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner
{
    public interface IShortcutController : IDisposable
    {
        void RegisterShortcutAction(ShortcutDescription shortcutDescription, Action action);
    }

    public class ShortcutController : IShortcutController
    {
        private readonly IShortcutCollection _shortcutCollection;
        private readonly IKeyboardHook _keyboardHook;

        public ShortcutController(IShortcutCollection shortcutCollection, IKeyboardHook keyboardHook)
        {
            _shortcutCollection = shortcutCollection;
            _keyboardHook = keyboardHook;

            keyboardHook.KeyPressed += KeyboardHookOnKeyPressed;
        }

        private void KeyboardHookOnKeyPressed(object sender, KeyPressedEventArgs keyPressedEventArgs)
        {
            var actions = _shortcutCollection.GetActions(keyPressedEventArgs.ShortcutDescription);

            foreach (var action in actions)
            {
                action();
            }
        }

        public void RegisterShortcutAction(ShortcutDescription shortcutDescription, Action action)
        {
            _shortcutCollection.Add(shortcutDescription, action);
        }

        public void Dispose()
        {
            _keyboardHook.Dispose();
        }
    }
}