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
                Shortcut = new ShortcutDescription(ModifierKeys.Alt, Keys.Z)
            };

            var secondLine = new ConfigurationLine
            {
                Shortcut = new ShortcutDescription(ModifierKeys.Alt, Keys.Y)
            };

            A.CallTo(() => sut.ConfigurationManager.ReadConfigurationFile())
                .Returns(new[] { firstLine, secondLine });

            // Act

            sut.Initialize();

            // Assert

            // TODO: Test actions somehow.

            A.CallTo(() => sut.ShortcutController.RegisterShortcutAction(firstLine.Shortcut, A<Action>._))
                .MustHaveHappened();

            A.CallTo(() => sut.ShortcutController.RegisterShortcutAction(secondLine.Shortcut, A<Action>._))
                .MustHaveHappened();
        }
    }
}