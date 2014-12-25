using System;
using System.Windows.Forms;
using FakeItEasy;
using NUnit.Framework;
using ShortcutRunner.HotkeyRegistration;
using ShortcutRunner.ShortcutDescriptionParsing;

namespace ShortcutRunner.Tests.ShortcutDescriptionParsing
{
    public class ShortcutCreatorTests
    {
        public readonly ShortcutDescriptionCreator Sut = new ShortcutDescriptionCreator(
            A.Fake<IShortcutParser>(),
            A.Fake<IKeyTokensValidator>(),
            A.Fake<IShortcutDescriptionFactory>());

        [Test]
        public void Can_Create_Shortcut()
        {
            // Arrange

            var shortcut = "Shift + K";
            
            var keyTokens = new IKeyToken[] {};

            A.CallTo(() => Sut.ShortcutParser.Parse(shortcut))
                .Returns(keyTokens);

            var shortcutDescription = new ShortcutDescription(ModifierKeys.Shift, Keys.K);

            A.CallTo(() => Sut.ShortcutDescriptionFactory.Create(keyTokens))
                .Returns(shortcutDescription);

            // Act

            var result = Sut.Create(shortcut);

            // Assert

            Assert.That(result, Is.SameAs(shortcutDescription));
        }

        [Test]
        public void Throws_Exception_When_Input_Is_Null()
        {
            Assert.That(() => Sut.Create(null),
                Throws.TypeOf<ArgumentNullException>());
        }
    }
}