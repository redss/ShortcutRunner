using System.Windows.Forms;
using NUnit.Framework;
using ShortcutRunner.HotkeyRegistration;
using ShortcutRunner.ShortcutDescriptionParsing;

namespace ShortcutRunner.Tests.ShortcutDescriptionParsing
{
    public class KeyParserTests
    {
        public KeyParser Sut = new KeyParser();

        [TestCase("A", Keys.A)]
        [TestCase("k", Keys.K)]
        [TestCase("F5", Keys.F5)]
        public void Can_Parse_Non_Modifier_Key(string keyString, Keys resultKey)
        {
            var result = Sut.Parse(keyString);

            var expectedToken = new KeyToken(resultKey);

            Assert.That(result, Is.EqualTo(expectedToken));
        }

        [Theory]
        public void Can_Parse_Modifier_Key(ModifierKeys modifierKey)
        {
            var result = Sut.Parse(modifierKey.ToString());

            var expectedToken = new ModifierKeyToken(modifierKey);

            Assert.That(result, Is.EqualTo(expectedToken));
        }

        [Test]
        public void Throws_Exception_When_Key_Is_Invalid()
        {
            var exception = Assert.Throws<KeyNotRecognizedException>(() =>
                Sut.Parse("Invalid Key"));
            
            Assert.That(exception.NotRecognizedKey, Is.EqualTo("Invalid Key"));
        }
    }
}