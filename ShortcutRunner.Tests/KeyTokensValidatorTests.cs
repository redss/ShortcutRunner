using System.Windows.Forms;
using NUnit.Framework;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner.Tests
{
    class KeyTokensValidatorTests
    {
        public KeyTokensValidator Sut = new KeyTokensValidator();

        [Test]
        public void Can_Validate_Key_Tokens()
        {
            var input = new IKeyToken[]
            {
                new ModifierKeyToken(ModifierKeys.Ctrl),
                new KeyToken(Keys.K)
            };

            Assert.That(() => Sut.Validate(input),
                Throws.Nothing);
        }

        [Test]
        public void Throws_Exception_When_Non_Modifier_Keys_Are_Used_More_Than_Once()
        {
            var input = new IKeyToken[]
            {
                new ModifierKeyToken(ModifierKeys.Ctrl),
                new KeyToken(Keys.K),
                new KeyToken(Keys.A)
            };

            Assert.That(() => Sut.Validate(input),
                Throws.TypeOf<MultipleNonModifierKeysException>());
        }

        [Test]
        public void Throws_Exception_When_There_Is_No_Non_Modifier_Key()
        {
            var input = new IKeyToken[]
            {
                new ModifierKeyToken(ModifierKeys.Ctrl),
                new ModifierKeyToken(ModifierKeys.Alt)
            };

            Assert.That(() => Sut.Validate(input),
                Throws.TypeOf<NoNonModifierKeysException>());
        }
    }
}