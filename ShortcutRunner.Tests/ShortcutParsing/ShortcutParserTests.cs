using System;
using System.Windows.Forms;
using NUnit.Framework;
using ShortcutRunner.HotkeyRegistration;
using ShortcutRunner.ShortcutParsing;

namespace ShortcutRunner.Tests.ShortcutParsing
{
    class ShortcutParserTests
    {
        public ShortcutParser Sut;

        [SetUp]
        public void SetUp()
        {
            Sut = new ShortcutParser(new KeyParser());
        }

        public dynamic[] ValidTestCases =
        {
            new
            {
                ShortcutText = "Ctrl + Shift + C",
                ExpectedResult = new ShortcutDescription(ModifierKeys.Ctrl | ModifierKeys.Shift, Keys.C)
            },
            new
            {
                ShortcutText = "alt + shift + f12",
                ExpectedResult = new ShortcutDescription(ModifierKeys.Alt | ModifierKeys.Shift, Keys.F12)
            },
            new
            {
                ShortcutText = "Z",
                ExpectedResult = new ShortcutDescription(ModifierKeys.None, Keys.Z)
            },
            new
            {
                ShortcutText = " Alt+Ctrl+  X  ",
                ExpectedResult = new ShortcutDescription(ModifierKeys.Alt | ModifierKeys.Ctrl, Keys.X)
            }
        };

        [TestCaseSource("ValidTestCases")]
        public void Can_Parse_Shortcut_Description(dynamic testCase)
        {
            var result = Sut.Create(testCase.ShortcutText);

            Assert.That(result, Is.EqualTo(testCase.ExpectedResult));
        }

        [Test]
        public void Throws_Exception_When_Input_Is_Null()
        {
            Assert.That(() => Sut.Create(null),
                Throws.TypeOf<ArgumentNullException>());
        }

        [TestCase("Ctrl + InvalidKey", "InvalidKey")]
        [TestCase("Blah + Z", "Blah")]
        public void Throws_Exception_When_Key_Is_Invalid(string shortcut, string invalidKey)
        {
            var exception = Assert.Throws<KeyNotRecognizedException>(() =>
                Sut.Create(shortcut));

            Assert.That(exception.NotRecognizedKey, Is.EqualTo(invalidKey));
            Assert.That(exception.Shortcut, Is.EqualTo(shortcut));
        }

        [TestCase("Ctrl + K + A")]
        [TestCase("Shift + F1 + F2")]
        [TestCase("A + B + C")]
        public void Throws_Exception_When_Non_Modifier_Keys_Are_Used_More_Than_Once(string shortcut)
        {
            var exception = Assert.Throws<MultipleNonModifierKeysException>(() =>
                Sut.Create(shortcut));

            Assert.That(exception.Shortcut, Is.EqualTo(shortcut));
        }

        [TestCase("Ctrl + Alt")]
        [TestCase("Shift + Win")]
        public void Throws_Exception_When_There_Is_No_Non_Modifier_Key(string shortcut)
        {
            var exception = Assert.Throws<NoNonModifierKeysException>(() =>
                Sut.Create(shortcut));

            Assert.That(exception.Shortcut, Is.EqualTo(shortcut));
        }
    }
}