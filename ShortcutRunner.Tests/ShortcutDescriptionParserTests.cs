using System;
using System.Windows.Forms;
using NUnit.Framework;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner.Tests
{
    public class ShortcutDescriptionParserTests
    {
        public ShortcutDescriptionParser Sut = new ShortcutDescriptionParser();

        [Test]
        public void Can_Parse_Shortcut_Description()
        {
            var result = Sut.Parse("Ctrl + Shift + C");

            Assert.That(result.Modifiers, Is.EqualTo(ModifierKeys.Ctrl | ModifierKeys.Shift));
            Assert.That(result.Key, Is.EqualTo(Keys.C));
        }

        [Test]
        public void Can_Parse_Other_Description()
        {
            var result = Sut.Parse("Alt + Shift + F2");

            Assert.That(result.Modifiers, Is.EqualTo(ModifierKeys.Alt | ModifierKeys.Shift));
            Assert.That(result.Key, Is.EqualTo(Keys.F2));
        }

        [Test]
        public void Throws_Exception_When_Non_Modifier_Keys_Are_Used_More_Than_Once()
        {
            Assert.That(() => Sut.Parse("Ctrl + A + B"),
                Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void Throws_Exception_When_Input_Is_Invalid()
        {
            Assert.That(() => Sut.Parse("invalid shortcut descirption"),
                Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void Throws_Exception_When_Input_Is_Null()
        {
            Assert.That(() => Sut.Parse(null),
                Throws.TypeOf<ArgumentNullException>());
        }
    }
}