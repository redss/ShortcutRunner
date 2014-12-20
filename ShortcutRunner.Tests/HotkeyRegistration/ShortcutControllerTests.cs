using System;
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

            var shortcutDescription = ShortcutDescription.Ctrl(Keys.A);

            var action = A.Fake<Action>();

            var otherShortcutDescription = ShortcutDescription.Shift(Keys.A);

            var otherAction = A.Fake<Action>();

            sut.RegisterShortcutAction(shortcutDescription, action);
            sut.RegisterShortcutAction(otherShortcutDescription, otherAction);

            // Act

            keyboardHook.KeyPressed += Raise.With(new KeyPressedEventArgs(ShortcutDescription.Ctrl(Keys.A)));

            // Assert

            A.CallTo(() => action()).MustHaveHappened();
            A.CallTo(() => otherAction()).MustNotHaveHappened();
        }
    }
}