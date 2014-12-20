using System;
using System.Windows.Forms;
using FakeItEasy;
using NUnit.Framework;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner.Tests
{
    public class ShortcutCollectionTests
    {
        [Test]
        public void Can_Add_Shortcut()
        {
            var sut = new ShortcutCollection();

            var action = A.Fake<Action>();

            sut.Add(ShortcutDescription.Shift(Keys.A), action);

            var actions = sut.GetActions(ShortcutDescription.Shift(Keys.A));

            Assert.That(actions, Is.EquivalentTo(new[] { action }));
        }

        [Test]
        public void Can_Add_Multiple_Sam_Shortcuts()
        {
            var sut = new ShortcutCollection();

            var firstAction = A.Fake<Action>();
            var secondAction = A.Fake<Action>();

            sut.Add(ShortcutDescription.Ctrl(Keys.K), firstAction);
            sut.Add(ShortcutDescription.Ctrl(Keys.K), secondAction);

            var result = sut.GetActions(ShortcutDescription.Ctrl(Keys.K));

            Assert.That(result, Is.EquivalentTo(new[] { firstAction, secondAction }));
        }
    }
}