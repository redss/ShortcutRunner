using System;
using FakeItEasy;
using NUnit.Framework;

namespace ShortcutRunner.Tests
{
    class KeyRegistrationControllerTests
    {
        [Test]
        public void Can_Register_Multiple_Hotkeys()
        {
            // Arrange

            var fixture = new KeyRegistrationControllerSutFactory();

            var sut = fixture.CreateSut();

            var windowHandle = new IntPtr(123);
            var shortcut = new ShortcutDescription();
            var otherShortcut = new ShortcutDescription();

            // Act

            sut.RegisterHotKey(windowHandle, shortcut);
            sut.RegisterHotKey(windowHandle, otherShortcut);

            // Assert

            A.CallTo(() => fixture.KeyRegistrationWrapper.RegisterHotKey(windowHandle, 1, shortcut))
                .MustHaveHappened();
            
            A.CallTo(() => fixture.KeyRegistrationWrapper.RegisterHotKey(windowHandle, 2, otherShortcut))
                .MustHaveHappened();
        }

        [Test]
        public void Can_Unregister_All_Hotkeys_On_Dispose()
        {
            // Arrange

            var fixture = new KeyRegistrationControllerSutFactory();

            var sut = fixture.CreateSut();

            sut.RegisterHotKey(new IntPtr(1), new ShortcutDescription());
            sut.RegisterHotKey(new IntPtr(2), new ShortcutDescription());
            sut.RegisterHotKey(new IntPtr(3), new ShortcutDescription());

            // Act

            sut.Dispose();

            // Assert

            A.CallTo(() => fixture.KeyRegistrationWrapper.UnregisterHotKey(new IntPtr(1), 1))
                .MustHaveHappened();

            A.CallTo(() => fixture.KeyRegistrationWrapper.UnregisterHotKey(new IntPtr(2), 2))
                .MustHaveHappened();

            A.CallTo(() => fixture.KeyRegistrationWrapper.UnregisterHotKey(new IntPtr(3), 3))
                .MustHaveHappened();
        }
    }

    class KeyRegistrationControllerSutFactory
    {
        public readonly IKeyRegistrationWrapper KeyRegistrationWrapper = A.Fake<IKeyRegistrationWrapper>();

        public KeyRegistrationController CreateSut()
        {
            return new KeyRegistrationController(KeyRegistrationWrapper);
        }
    }
}