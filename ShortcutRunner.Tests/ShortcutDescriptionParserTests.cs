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
            var result = Sut.Parse("Ctrl + Shift + F2");

            Assert.That(result, Is.EqualTo(new IKeyToken[]
            {
                new ModifierKeyToken(ModifierKeys.Ctrl),
                new ModifierKeyToken(ModifierKeys.Shift),
                new KeyToken(Keys.F2)
            }));
        }

        [Test]
        public void Throws_Exception_When_Key_Is_Invalid()
        {
            Assert.That(() => Sut.Parse("Ctrl + Invalid Key"),
                Throws.TypeOf<KeyNotRecognizedException>()); // todo check invalid key string
        }

    }
}