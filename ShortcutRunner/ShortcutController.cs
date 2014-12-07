using System;
using System.Collections.Generic;
using System.Linq;

namespace ShortcutRunner
{
    public interface IShortcutController
    {
        void RegisterShortcutAction(ShortcutDescription shortcutDescription, Action action);
    }

    public class ShortcutController : IShortcutController
    {
        private readonly IList<KeyValuePair<ShortcutDescription, Action>> _actions =
            new List<KeyValuePair<ShortcutDescription, Action>>();

        public ShortcutController(IKeyboardHook keyboardHook)
        {
            keyboardHook.KeyPressed += KeyboardHookOnKeyPressed;
        }

        private void KeyboardHookOnKeyPressed(object sender, KeyPressedEventArgs keyPressedEventArgs)
        {
            var actions = _actions
                .Where(k => keyPressedEventArgs.ShortcutDescription.Equals(k.Key))
                .Select(k => k.Value)
                .ToArray();

            foreach (var action in actions)
            {
                action();
            }
        }

        public void RegisterShortcutAction(ShortcutDescription shortcutDescription, Action action)
        {
            _actions.Add(new KeyValuePair<ShortcutDescription, Action>(shortcutDescription, action));
        }
    }
}