using System;
using System.Windows.Forms;
using FakeItEasy;
using NUnit.Framework;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner.Tests.HotkeyRegistration
{
    class KeyboardHookTests
    {
        public readonly KeyboardHook Sut = SutFactory.Create<KeyboardHook>();

        [Test]
        public void Can_Register_A_Hot_Key()
        {
            // Arrange

            var windowIntPtr = new IntPtr(123);
            var input = ShortcutDescription.Shift(Keys.A);

            A.CallTo(() => Sut.MessageCatchingWindow.Handle)
                .Returns(windowIntPtr);

            // Act

            Sut.RegisterHotKey(input);

            // Assert

            A.CallTo(() => Sut.KeyRegistrationController.RegisterHotKey(windowIntPtr, input))
                .MustHaveHappened();
        }

        [Test]
        public void Can_Notify_About_Hotkey_Event()
        {
            // Arrange

            var observer = A.Fake<EventHandler<KeyPressedEventArgs>>();
            var eventArgs = new KeyPressedEventArgs(ShortcutDescription.Shift(Keys.A));

            // Act

            Sut.KeyPressed += observer;
            Sut.MessageCatchingWindow.KeyPressed += Raise.With(eventArgs);

            // Assert

            A.CallTo(() => observer(Sut, eventArgs))
                .MustHaveHappened();
        }

        [Test]
        public void Can_Work_Without_Registered_Event_Listeners()
        {
            // Arrange

            var eventArgs = new KeyPressedEventArgs(ShortcutDescription.Shift(Keys.A));

            // Act

            Sut.MessageCatchingWindow.KeyPressed += Raise.With(eventArgs);

            // Assert

            Assert.Pass("Works when no event listeners were registered.");
        }
    }
}