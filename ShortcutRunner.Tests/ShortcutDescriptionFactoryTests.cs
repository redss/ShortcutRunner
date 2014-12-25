using System.Windows.Forms;
using NUnit.Framework;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner.Tests
{
    class ShortcutDescriptionFactoryTests
    {
        public readonly ShortcutDescriptionFactory Sut = new ShortcutDescriptionFactory();

        [Test]
        public void Can_Create_Shortcut_Description()
        {
            // TODO: Multiple tests.

            var input = new IKeyToken[]
            {
                new ModifierKeyToken(ModifierKeys.Ctrl),
                new ModifierKeyToken(ModifierKeys.Alt),
                new KeyToken(Keys.T)
            };

            var result = Sut.Create(input);

            Assert.That(result, Is.EqualTo(new ShortcutDescription(
                ModifierKeys.Ctrl | ModifierKeys.Alt,
                Keys.T)));
        }
    }
}