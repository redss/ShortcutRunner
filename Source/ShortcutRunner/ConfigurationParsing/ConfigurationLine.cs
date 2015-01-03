using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner.ConfigurationParsing
{
    public class ConfigurationLine
    {
        public ShortcutDescription Shortcut { get; set; }
        public string Command { get; set; }
    }
}