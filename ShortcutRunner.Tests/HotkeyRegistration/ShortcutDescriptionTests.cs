using System.Windows.Forms;
using NUnit.Framework;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner.Tests.HotkeyRegistration
{
    class ShortcutDescriptionEqualityTests
    {
        [Datapoints]
        public Keys[] KeysDatapoints =
        {
            Keys.A, Keys.B, Keys.C
        };

        [Datapoints]
        public ModifierKeys[] ModifierKeysDatapoints =
        {
            ModifierKeys.Ctrl, ModifierKeys.Alt, ModifierKeys.Shift, ModifierKeys.Alt | ModifierKeys.Ctrl
        };

        [Theory]
        public void Can_Detect_Equal_Shortcuts(ModifierKeys firstModifiers, Keys firstKey, ModifierKeys secondModifiers, Keys secondKey)
        {
            Assume.That(firstModifiers == secondModifiers && firstKey == secondKey);

            var firstShortcut = new ShortcutDescription(firstModifiers, firstKey);
            var secondShortcut = new ShortcutDescription(secondModifiers, secondKey);

            Assert.That(firstShortcut, Is.EqualTo(secondShortcut));
            Assert.That(firstShortcut.GetHashCode(), Is.EqualTo(secondShortcut.GetHashCode()));
        }

        [Theory]
        public void Can_Detect_Different_Shortcuts(ModifierKeys firstModifiers, Keys firstKey, ModifierKeys secondModifiers, Keys secondKey)
        {
            Assume.That(firstModifiers != secondModifiers || firstKey != secondKey);

            var firstShortcut = new ShortcutDescription(firstModifiers, firstKey);
            var secondShortcut = new ShortcutDescription(secondModifiers, secondKey);

            Assert.That(firstShortcut, Is.Not.EqualTo(secondShortcut));
            Assert.That(firstShortcut.GetHashCode(), Is.Not.EqualTo(secondShortcut.GetHashCode()));
        }

        public dynamic[] ToStringTestCases =
        {
            new
            {
                Shortcut = new ShortcutDescription(ModifierKeys.Ctrl, Keys.Z),
                ExpectedString = "Ctrl + Z"
            },
            new
            {
                Shortcut = new ShortcutDescription(ModifierKeys.Ctrl | ModifierKeys.Alt, Keys.X),
                ExpectedString = "Ctrl + Alt + X"
            }
        };

        [TestCaseSource("ToStringTestCases")]
        public void Can_Turn_Shortcut_Description_To_String(dynamic testCase)
        {
            Assert.That(testCase.Shortcut.ToString(), Is.EqualTo(testCase.ExpectedString));
        }
    }
}