﻿using System;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner
{
    public interface IShortcutController : IDisposable
    {
        void RegisterShortcutAction(ShortcutDescription shortcutDescription, Action action);
    }

    public class ShortcutController : IShortcutController
    {
        public readonly IShortcutCollection ShortcutCollection;
        public readonly IKeyboardHook KeyboardHook;

        public ShortcutController(IShortcutCollection shortcutCollection, IKeyboardHook keyboardHook)
        {
            ShortcutCollection = shortcutCollection;
            KeyboardHook = keyboardHook;

            keyboardHook.KeyPressed += KeyboardHookOnKeyPressed;
        }

        private void KeyboardHookOnKeyPressed(object sender, KeyPressedEventArgs keyPressedEventArgs)
        {
            var actions = ShortcutCollection.GetActions(keyPressedEventArgs.ShortcutDescription);

            foreach (var action in actions)
            {
                action();
            }
        }

        public void RegisterShortcutAction(ShortcutDescription shortcutDescription, Action action)
        {
            ShortcutCollection.Add(shortcutDescription, action);
        }

        public void Dispose()
        {
            KeyboardHook.Dispose();
        }
    }
}