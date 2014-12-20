﻿using System;
using System.Windows.Forms;
using FakeItEasy;
using NUnit.Framework;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner.Tests.HotkeyRegistration
{
    public class ShortcutControllerTests
    {
        [Test]
        public void Fires_Action_On_Registered_Shortcut()
        {
            // Arrange

            var keyboardHook = A.Fake<IKeyboardHook>();
            var sut = new ShortcutController(keyboardHook);

            var shortcutDescription = new ShortcutDescription
            {
                Modifiers = ModifierKeys.Shift | ModifierKeys.Alt,
                Key = Keys.F12
            };

            var action = A.Fake<Action>();

            var otherShortcutDescription = new ShortcutDescription
            {
                Modifiers = ModifierKeys.Shift,
                Key = Keys.A
            };

            var otherAction = A.Fake<Action>();

            sut.RegisterShortcutAction(shortcutDescription, action);
            sut.RegisterShortcutAction(otherShortcutDescription, otherAction);

            // Act

            var actualShortcut = new ShortcutDescription
            {
                Modifiers = ModifierKeys.Shift | ModifierKeys.Alt,
                Key = Keys.F12
            };

            keyboardHook.KeyPressed += Raise.With(new KeyPressedEventArgs(actualShortcut));

            // Assert

            A.CallTo(() => action()).MustHaveHappened();
            A.CallTo(() => otherAction()).MustNotHaveHappened();
        }
    }
}