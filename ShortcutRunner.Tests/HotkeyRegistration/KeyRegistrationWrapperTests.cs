using System;
using System.Windows.Forms;
using FakeItEasy;
using NUnit.Framework;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner.Tests.HotkeyRegistration
{
    class KeyRegistrationWrapperTests
    {
        [Test]
        public void Throws_Exception_When_Registration_Fails()
        {
            // Arrange

            var fixture = new KeyRegistrationWrapperFixture()
                .WithFailingHotkeyRegistration()
                .Configure();

            var shortcut = new ShortcutDescription(ModifierKeys.Shift, Keys.K);

            // Act, Assert

            var exception = Assert.Throws<HotkeyRegistrationException>(() => 
                fixture.Sut.RegisterHotKey(new IntPtr(123), 12, shortcut));

            Assert.That(exception.ShortcutDescription, Is.SameAs(shortcut));
        }

        [Test]
        public void Can_Register_A_Hotkey()
        {
            // Arrange

            var fixture = new KeyRegistrationWrapperFixture()
                .Configure();

            // Act

            fixture.Sut.RegisterHotKey(new IntPtr(123), 12, ShortcutDescription.Shift(Keys.A));

            // Assert

            fixture.VerifyKeyWasRegistered(new IntPtr(123), 12, (uint) ModifierKeys.Shift, (uint) Keys.A);
        }

        [Test]
        public void Can_Unregister_A_Hotkey()
        {
            // Arrange

            var fixture = new KeyRegistrationWrapperFixture()
                .Configure();

            // Act

            fixture.Sut.UnregisterHotKey(new IntPtr(123), 12);

            // Assert

            fixture.VerifyKeyWasUnregistered(new IntPtr(123), 12);
        }
    }

    class KeyRegistrationWrapperFixture
    {
        public KeyRegistrationWrapper Sut = SutFactory.Create<KeyRegistrationWrapper>();

        private bool _registerHotKeyResult = true;

        public KeyRegistrationWrapperFixture WithFailingHotkeyRegistration()
        {
            _registerHotKeyResult = false;
            return this;
        }

        public KeyRegistrationWrapperFixture Configure()
        {
            A.CallTo(() => Sut.KeyRegistrationApi.RegisterHotKey(A<IntPtr>._, A<int>._, A<uint>._, A<uint>._))
                .Returns(_registerHotKeyResult);

            return this;
        }

        public void VerifyKeyWasRegistered(IntPtr hWnd, int id, uint fsModifiers, uint vk)
        {
            A.CallTo(() => Sut.KeyRegistrationApi.RegisterHotKey(hWnd, id, fsModifiers, vk))
                .MustHaveHappened();
        }

        public void VerifyKeyWasUnregistered(IntPtr hWnd, int id)
        {
            A.CallTo(() => Sut.KeyRegistrationApi.UnregisterHotKey(hWnd, id))
                .MustHaveHappened();
        }
    }
}