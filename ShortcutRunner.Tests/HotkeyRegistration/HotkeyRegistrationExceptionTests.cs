using System.Windows.Forms;
using NUnit.Framework;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner.Tests.HotkeyRegistration
{
    class HotkeyRegistrationExceptionTests
    {
        [Test]
        public void Can_Format_HotkeyRegistrationException_Message()
        {
            var sut = new HotkeyRegistrationException
            {
                ShortcutDescription = new ShortcutDescription(ModifierKeys.Shift, Keys.X)
            };

            Assert.That(sut.Message, Is.EqualTo("There was an error while registering 'Shift + X' shortcut in Windows."));
        }
    }
}