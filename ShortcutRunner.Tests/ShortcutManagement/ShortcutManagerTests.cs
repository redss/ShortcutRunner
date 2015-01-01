using System;
using System.Windows.Forms;
using FakeItEasy;
using NUnit.Framework;
using ShortcutRunner.ConfigurationParsing;
using ShortcutRunner.HotkeyRegistration;
using ShortcutRunner.ShortcutManagement;

namespace ShortcutRunner.Tests.ShortcutManagement
{
    class ShortcutManagerTests
    {
        [Test]
        public void Can_Initialize_Shortcuts()
        {
            // Arrange

            var sut = SutFactory.Create<ShortcutManager>();

            var firstLine = new ConfigurationLine
            {
                Shortcut = new ShortcutDescription(ModifierKeys.Alt, Keys.Z),
                Command = "first command"
            };

            var secondLine = new ConfigurationLine
            {
                Shortcut = new ShortcutDescription(ModifierKeys.Alt, Keys.Y),
                Command = "second command"
            };

            A.CallTo(() => sut.ConfigurationManager.ReadConfigurationFile())
                .Returns(new[] { firstLine, secondLine });

            var firstAction = A.Fake<Action>();
            var secondAction = A.Fake<Action>();

            A.CallTo(() => sut.CommandActionProvider.CreateCommandAction(firstLine.Command))
                .Returns(firstAction);
            
            A.CallTo(() => sut.CommandActionProvider.CreateCommandAction(secondLine.Command))
                .Returns(secondAction);

            // Act

            sut.Initialize();

            // Assert

            A.CallTo(() => sut.ShortcutController.RegisterShortcutAction(firstLine.Shortcut, firstAction))
                .MustHaveHappened();

            A.CallTo(() => sut.ShortcutController.RegisterShortcutAction(secondLine.Shortcut, secondAction))
                .MustHaveHappened();
        }
    }
}