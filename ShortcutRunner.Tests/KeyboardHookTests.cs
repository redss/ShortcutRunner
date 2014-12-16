using System;
using FakeItEasy;
using NUnit.Framework;

namespace ShortcutRunner.Tests
{
    class KeyboardHookTests
    {
        [Test]
        public void Can_Register_A_Hot_Key()
        {
            // Arrange

            var fixture = new KeyboardHookFixture();
            var sut = fixture.CreateSut();
            var windowIntPtr = new IntPtr(123);
            var input = new ShortcutDescription();

            A.CallTo(() => fixture.MessageCatchingWindow.Handle)
                .Returns(windowIntPtr);

            // Act

            sut.RegisterHotKey(input);

            // Assert

            A.CallTo(() => fixture.KeyRegistrationController.RegisterHotKey(windowIntPtr, input))
                .MustHaveHappened();
        }

        [Test]
        public void Can_Notify_About_Hotkey_Event()
        {
            // Arrange

            var fixture = new KeyboardHookFixture();
            var sut = fixture.CreateSut();

            var observer = A.Fake<EventHandler<KeyPressedEventArgs>>();
            var eventArgs = new KeyPressedEventArgs(new ShortcutDescription());

            // Act

            sut.KeyPressed += observer;
            fixture.MessageCatchingWindow.KeyPressed += Raise.With(eventArgs);

            // Assert

            A.CallTo(() => observer(sut, eventArgs))
                .MustHaveHappened();
        }

        [Test]
        public void Can_Work_Without_Registered_Event_Listeners()
        {
            // Arrange

            var fixture = new KeyboardHookFixture();
            var sut = fixture.CreateSut();

            var eventArgs = new KeyPressedEventArgs(new ShortcutDescription());

            // Act

            fixture.MessageCatchingWindow.KeyPressed += Raise.With(eventArgs);

            // Assert

            Assert.Pass("Works, when no event listeners were registered.");
        }

        [Test]
        public void Can_Dispose_Window_And_Key_Registration_Controller()
        {
            // Arrange

            var fixture = new KeyboardHookFixture();
            var sut = fixture.CreateSut();

            // Act

            sut.Dispose();

            // Assert

            A.CallTo(() => fixture.MessageCatchingWindow.Dispose())
                .MustHaveHappened();

            A.CallTo(() => fixture.KeyRegistrationController.Dispose())
                .MustHaveHappened();
        }
    }

    class KeyboardHookFixture
    {
        public readonly IMessageCatchingWindow MessageCatchingWindow = A.Fake<IMessageCatchingWindow>();
        public readonly IKeyRegistrationController KeyRegistrationController = A.Fake<IKeyRegistrationController>();

        public KeyboardHook CreateSut()
        {
            return new KeyboardHook(MessageCatchingWindow, KeyRegistrationController);
        }
    }
}