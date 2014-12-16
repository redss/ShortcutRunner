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

            A.CallTo(() => fixture.MessageCatchingWindow.Handle)
                .Returns(windowIntPtr);

            var input = new ShortcutDescription();

            // Act

            sut.RegisterHotKey(input);

            // Assert

            A.CallTo(() => fixture.KeyRegistrationController.RegisterHotKey(windowIntPtr, input))
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