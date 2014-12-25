using System;
using System.Windows.Forms;
using FakeItEasy;
using NUnit.Framework;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner.Tests.HotkeyRegistration
{
    public class ShortcutControllerTests
    {
        public readonly ShortcutController Sut = SutFactory.Create<ShortcutController>();

        [Test]
        public void Calls_All_Actions_Related_To_Shortcut_On_Keypressed_Event()
        {
            // Arrange

            var firstAction = A.Fake<Action>();
            var secondAction = A.Fake<Action>();

            var shortcutDescription = ShortcutDescription.Shift(Keys.A);

            A.CallTo(() => Sut.ShortcutCollection.GetActions(shortcutDescription))
                .Returns(new[] { firstAction, secondAction });

            // Act

            Sut.KeyboardHook.KeyPressed += Raise.With(new KeyPressedEventArgs(shortcutDescription));

            // Assert

            A.CallTo(firstAction).MustHaveHappened();
            A.CallTo(secondAction).MustHaveHappened();
        }

        [Test]
        public void Can_Add_Action_Related_To_Shortcut()
        {
            // Arrange

            var shortcutDescription = ShortcutDescription.Shift(Keys.A);
            var action = A.Fake<Action>();

            // Act

            Sut.RegisterShortcutAction(shortcutDescription, action);

            // Assert

            A.CallTo(() => Sut.ShortcutCollection.Add(shortcutDescription, action))
                .MustHaveHappened();
        }

        [Test]
        public void Disposes_Keyboard_Hook_When_Disposed()
        {
            // Act

            Sut.Dispose();

            // Assert

            A.CallTo(() => Sut.KeyboardHook.Dispose())
                .MustHaveHappened();
        }
    }
}