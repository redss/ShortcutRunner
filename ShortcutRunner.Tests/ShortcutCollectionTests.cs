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

            sut.Add(new ShortcutDescription { Key = Keys.A, Modifiers = ModifierKeys.Shift}, action);

            var actions = sut.GetActions(new ShortcutDescription {Key = Keys.A, Modifiers = ModifierKeys.Shift});

            Assert.That(actions, Is.EquivalentTo(new[] { action }));
        }

        [Test]
        public void Can_Add_Multiple_Sam_Shortcuts()
        {
            var sut = new ShortcutCollection();

            var firstAction = A.Fake<Action>();
            var secondAction = A.Fake<Action>();

            sut.Add(new ShortcutDescription {Key = Keys.K, Modifiers = ModifierKeys.Ctrl}, firstAction);
            sut.Add(new ShortcutDescription {Key = Keys.K, Modifiers = ModifierKeys.Ctrl}, secondAction);

            var result = sut.GetActions(new ShortcutDescription {Key = Keys.K, Modifiers = ModifierKeys.Ctrl});

            Assert.That(result, Is.EquivalentTo(new[] { firstAction, secondAction }));
        }
    }
}