using NUnit.Framework;
using ShortcutRunner.ShortcutParsing;

namespace ShortcutRunner.Tests.ShortcutParsing
{
    class ShortcutParsingExceptionsTests
    {
        [Test]
        public void Can_Format_NoNonModifierKeysException_Message()
        {
            var sut = new NoNonModifierKeysException
            {
                Shortcut = "Ctrl + Shift"
            };

            Assert.That(sut.Message, Is.EqualTo("Shortcut 'Ctrl + Shift' contains no non-modifier key."));
        }

        [Test]
        public void Can_Format_KeyNotRecognizedException_Message()
        {
            var sut = new KeyNotRecognizedException
            {
                Shortcut = "Ctrl + Beefcake",
                NotRecognizedKey = "Beefcake"
            };

            Assert.That(sut.Message, Is.EqualTo("Key 'Beefcake' was not recognized in 'Ctrl + Beefcake' shortcut."));
        }

        [Test]
        public void Can_Format_MultipleNonModifierKeysException_Message()
        {
            var sut = new MultipleNonModifierKeysException
            {
                Shortcut = "Ctrl + A + Z"
            };

            Assert.That(sut.Message, Is.EqualTo("Shortcut 'Ctrl + A + Z' conatins more than one non-modifier key."));
        }
    }
}