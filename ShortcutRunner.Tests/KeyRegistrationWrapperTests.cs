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

            var fixture = new KeyRegistrationWrapperFixture();

            A.CallTo(() => fixture.KeyRegistrationApi.RegisterHotKey(A<IntPtr>._, A<int>._, A<uint>._, A<uint>._))
                .Returns(false);

            // Act

            var sut = fixture.CreateSut();

            // Assert

            Assert.That(() => sut.RegisterHotKey(new IntPtr(123), 12, new ShortcutDescription()),
                Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Can_Register_A_Hotkey()
        {
            // Arrange

            var fixture = new KeyRegistrationWrapperFixture();

            A.CallTo(() => fixture.KeyRegistrationApi.RegisterHotKey(A<IntPtr>._, A<int>._, A<uint>._, A<uint>._))
                .Returns(true);

            var sut = fixture.CreateSut();

            // Act

            sut.RegisterHotKey(new IntPtr(123), 12, new ShortcutDescription
            {
                Modifiers = ModifierKeys.Shift,
                Key = Keys.A
            });

            // Assert

            A.CallTo(() => fixture.KeyRegistrationApi.RegisterHotKey(new IntPtr(123), 12, (uint) ModifierKeys.Shift, (uint) Keys.A))
                .MustHaveHappened();
        }

        [Test]
        public void Can_Unregister_A_Hotkey()
        {
            // Arrange

            var fixture = new KeyRegistrationWrapperFixture();

            var sut = fixture.CreateSut();

            // Act

            sut.UnregisterHotKey(new IntPtr(123), 12);

            // Assert

            A.CallTo(() => fixture.KeyRegistrationApi.UnregisterHotKey(new IntPtr(123), 12))
                .MustHaveHappened();
        }
    }

    class KeyRegistrationWrapperFixture
    {
        public IKeyRegistrationApi KeyRegistrationApi = A.Fake<IKeyRegistrationApi>();

        public KeyRegistrationWrapper CreateSut()
        {
            return new KeyRegistrationWrapper(KeyRegistrationApi);
        }
    }
}