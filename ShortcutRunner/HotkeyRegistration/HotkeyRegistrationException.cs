using System;
using ShortcutRunner.Properties;

namespace ShortcutRunner.HotkeyRegistration
{
    public class HotkeyRegistrationException : Exception
    {
        public ShortcutDescription ShortcutDescription { get; set; }

        public override string Message
        {
            get
            {
                return string.Format(Resources.HotkeyRegistrationException, ShortcutDescription);
            }
        }
    }
}