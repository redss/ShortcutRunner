using System;
using System.Windows.Forms;
using FakeItEasy;
using NUnit.Framework;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner.Tests.HotkeyRegistration
{
    public class ShortcutControllerTests
    {
        [Test]
        public void Calls_All_Actions_Related_To_Shortcut_On_Keypressed_Event()
        {
            // Arrange

            var fixture = new ShortcutControllerSutFactory();

            var firstAction = A.Fake<Action>();
            var secondAction = A.Fake<Action>();

            var shortcutDescription = ShortcutDescription.Shift(Keys.A);

            A.CallTo(() => fixture.ShortcutCollection.GetActions(shortcutDescription))
                .Returns(new[] { firstAction, secondAction });

            fixture.CreateSut();

            // Act

            fixture.KeyboardHook.KeyPressed += Raise.With(new KeyPressedEventArgs(shortcutDescription));

            // Assert

            A.CallTo(firstAction).MustHaveHappened();
            A.CallTo(secondAction).MustHaveHappened();
        }

        [Test]
        public void Can_Add_Action_Related_To_Shortcut()
        {
            // Arrange

            var fixture = new ShortcutControllerSutFactory();

            var sut = fixture.CreateSut();

            var shortcutDescription = ShortcutDescription.Shift(Keys.A);
            var action = A.Fake<Action>();

            // Act

            sut.RegisterShortcutAction(shortcutDescription, action);

            // Assert

            A.CallTo(() => fixture.ShortcutCollection.Add(shortcutDescription, action))
                .MustHaveHappened();
        }
    }

    class ShortcutControllerSutFactory
    {
        public IShortcutCollection ShortcutCollection = A.Fake<IShortcutCollection>();
        public IKeyboardHook KeyboardHook = A.Fake<IKeyboardHook>();

        public ShortcutController CreateSut()
        {
            return new ShortcutController(ShortcutCollection, KeyboardHook);
        }
    }
}