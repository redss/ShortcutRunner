using System.Windows.Forms;
using NUnit.Framework;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner.Tests.HotkeyRegistration
{
    class ShortcutDescriptionTests
    {
        [Datapoints]
        public ShortcutDescription[] ShortcutDescriptions =
        {
            ShortcutDescription.Shift(Keys.A),
            ShortcutDescription.Shift(Keys.B),
            ShortcutDescription.Ctrl(Keys.A),
            ShortcutDescription.Ctrl(Keys.B)
        };

        [Theory]
        public void Can_Detect_Equal_Shortcuts(ShortcutDescription a, ShortcutDescription b)
        {
            Assume.That(a.Modifiers == b.Modifiers && a.Key == b.Key);
            
            Assert.That(a, Is.EqualTo(b));
            Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
        }

        [Theory]
        public void Can_Detect_Different_Shortcuts(ShortcutDescription a, ShortcutDescription b)
        {
            Assume.That(a.Modifiers != b.Modifiers || a.Key != b.Key);
            
            Assert.That(a, Is.Not.EqualTo(b));
            Assert.That(a.GetHashCode(), Is.Not.EqualTo(b.GetHashCode()));
        }
    }
}