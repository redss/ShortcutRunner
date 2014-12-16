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

            A.CallTo(() => fixture.KeyRegistrationApi.RegisterHotKey(windowIntPtr, 1, input))
                .MustHaveHappened();
        }
    }

    class KeyboardHookFixture
    {
        public readonly IKeyRegistrationWrapper KeyRegistrationApi = A.Fake<IKeyRegistrationWrapper>();
        public readonly IMessageCatchingWindow MessageCatchingWindow = A.Fake<IMessageCatchingWindow>();

        public KeyboardHook CreateSut()
        {
            return new KeyboardHook(MessageCatchingWindow, KeyRegistrationApi);
        }
    }
}