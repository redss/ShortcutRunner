﻿using System;

namespace ShortcutRunner.HotkeyRegistration
{
    public interface IKeyboardHook
    {
        event EventHandler<KeyPressedEventArgs> KeyPressed;
        void RegisterHotKey(ShortcutDescription shortcutDescription);
    }

    public class KeyboardHook : IKeyboardHook
    {
        public event EventHandler<KeyPressedEventArgs> KeyPressed = (sender, args) => { };

        public readonly IMessageCatchingWindow MessageCatchingWindow;
        public readonly IKeyRegistrationController KeyRegistrationController;

        public KeyboardHook(IMessageCatchingWindow messageCatchingWindow, IKeyRegistrationController keyRegistrationController)
        {
            MessageCatchingWindow = messageCatchingWindow;
            KeyRegistrationController = keyRegistrationController;

            MessageCatchingWindow.KeyPressed += OnMessageCatchingWindowOnKeyPressed;
        }

        public void RegisterHotKey(ShortcutDescription shortcutDescription)
        {
            KeyRegistrationController.RegisterHotKey(MessageCatchingWindow.Handle, shortcutDescription);
        }

        private void OnMessageCatchingWindowOnKeyPressed(object sender, KeyPressedEventArgs args)
        {
            KeyPressed(this, args);
        }
    }
}