using System.Windows.Forms;
using NUnit.Framework;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner.Tests
{
    public class KeyParserTests
    {
        public KeyParser Sut = new KeyParser();

        [Datapoints]
        public Keys[] KeyDatapoints = { Keys.A, Keys.K, Keys.F5 };

        [Theory]
        public void Can_Parse_Non_Modifier_Key(Keys key)
        {
            var result = Sut.Parse(key.ToString());

            var expectedToken = new KeyToken(key);

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
            Assert.That(() => Sut.Parse("Invalid Key"),
                Throws.TypeOf<KeyNotRecognizedException>()
                .With.Property("NotRecognizedKey").EqualTo("Invalid Key"));
        }
    }
}