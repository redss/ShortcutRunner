using System;
using FakeItEasy;
using NUnit.Framework;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner.Tests.HotkeyRegistration
{
    class KeyRegistrationControllerTests
    {
        [Test]
        public void Can_Register_Multiple_Hotkeys()
        {
            // Arrange

            var fixture = new KeyRegistrationControllerFixture();

            var firstShortcut = new ShortcutDescription();
            var secondShortcut = new ShortcutDescription();

            // Act

            fixture.Sut.RegisterHotKey(new IntPtr(1), firstShortcut);
            fixture.Sut.RegisterHotKey(new IntPtr(2), secondShortcut);

            // Assert

            fixture.RegisterHotKeyWasCalled(new IntPtr(1), 1, firstShortcut);
            fixture.RegisterHotKeyWasCalled(new IntPtr(2), 2, secondShortcut);
        }

        [Test]
        public void Can_Unregister_All_Hotkeys_On_Dispose()
        {
            // Arrange

            var fixture = new KeyRegistrationControllerFixture();

            fixture.Sut.RegisterHotKey(new IntPtr(1), new ShortcutDescription());
            fixture.Sut.RegisterHotKey(new IntPtr(2), new ShortcutDescription());
            fixture.Sut.RegisterHotKey(new IntPtr(3), new ShortcutDescription());

            // Act

            fixture.Sut.Dispose();

            // Assert

            fixture.UnregisterHotKeyWasCalled(new IntPtr(1), 1);
            fixture.UnregisterHotKeyWasCalled(new IntPtr(2), 2);
            fixture.UnregisterHotKeyWasCalled(new IntPtr(3), 3);
        }
    }

    class KeyRegistrationControllerFixture
    {
        public KeyRegistrationController Sut;

        private readonly KeyRegistrationControllerSutFactory _sutFactory = new KeyRegistrationControllerSutFactory();

        public KeyRegistrationControllerFixture()
        {
            Sut = _sutFactory.CreateSut();
        }

        public void RegisterHotKeyWasCalled(IntPtr handle, int id, ShortcutDescription shortcutDescription)
        {
            A.CallTo(() => _sutFactory.KeyRegistrationWrapper.RegisterHotKey(handle, id, shortcutDescription))
                .MustHaveHappened();
        }

        public void UnregisterHotKeyWasCalled(IntPtr handle, int id)
        {
            A.CallTo(() => _sutFactory.KeyRegistrationWrapper.UnregisterHotKey(handle, id))
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