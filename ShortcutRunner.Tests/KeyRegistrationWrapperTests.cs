using System;
using System.Windows.Forms;
using FakeItEasy;
using NUnit.Framework;

namespace ShortcutRunner.Tests
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

            // Act, Assert

            Assert.That(() => fixture.Sut.RegisterHotKey(new IntPtr(123), 12, new ShortcutDescription()),
                Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Can_Register_A_Hotkey()
        {
            // Arrange

            var fixture = new KeyRegistrationWrapperFixture()
                .Configure();

            // Act

            fixture.Sut.RegisterHotKey(new IntPtr(123), 12, new ShortcutDescription
            {
                Modifiers = ModifierKeys.Shift,
                Key = Keys.A
            });

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
        public KeyRegistrationWrapper Sut;

        private KeyRegistrationWrapperSutFactory _sutFactory = new KeyRegistrationWrapperSutFactory();
        private bool _registerHotKeyResult = true;

        public KeyRegistrationWrapperFixture WithFailingHotkeyRegistration()
        {
            _registerHotKeyResult = false;
            return this;
        }

        public KeyRegistrationWrapperFixture Configure()
        {
            A.CallTo(() => _sutFactory.KeyRegistrationApi.RegisterHotKey(A<IntPtr>._, A<int>._, A<uint>._, A<uint>._))
                .Returns(_registerHotKeyResult);

            Sut = _sutFactory.CreateSut();

            return this;
        }

        public void VerifyKeyWasRegistered(IntPtr hWnd, int id, uint fsModifiers, uint vk)
        {
            A.CallTo(() => _sutFactory.KeyRegistrationApi.RegisterHotKey(hWnd, id, fsModifiers, vk))
                .MustHaveHappened();
        }

        public void VerifyKeyWasUnregistered(IntPtr hWnd, int id)
        {
            A.CallTo(() => _sutFactory.KeyRegistrationApi.UnregisterHotKey(hWnd, id))
                .MustHaveHappened();
        }
    }

    class KeyRegistrationWrapperSutFactory
    {
        public readonly IKeyRegistrationApi KeyRegistrationApi = A.Fake<IKeyRegistrationApi>();

        public KeyRegistrationWrapper CreateSut()
        {
            return new KeyRegistrationWrapper(KeyRegistrationApi);
        }
    }
}