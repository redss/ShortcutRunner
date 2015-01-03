using System;

namespace ShortcutRunner.HotkeyRegistration
{
    public interface IKeyRegistrationWrapper
    {
        void RegisterHotKey(IntPtr windowHandle, int hotkeyId, ShortcutDescription shortcutDescription);
        void UnregisterHotKey(IntPtr windowHandle, int hotkeyId);
    }

    public class KeyRegistrationWrapper : IKeyRegistrationWrapper
    {
        public readonly IKeyRegistrationApi KeyRegistrationApi;

        public KeyRegistrationWrapper(IKeyRegistrationApi keyRegistrationApi)
        {
            KeyRegistrationApi = keyRegistrationApi;
        }

        public void RegisterHotKey(IntPtr windowHandle, int hotkeyId, ShortcutDescription shortcutDescription)
        {
            var registrationSucceeded = KeyRegistrationApi.RegisterHotKey(windowHandle, hotkeyId,
                (uint) shortcutDescription.Modifiers, (uint) shortcutDescription.Key);

            if (!registrationSucceeded)
            {
                throw new HotkeyRegistrationException
                {
                    ShortcutDescription = shortcutDescription
                };
            }
        }

        public void UnregisterHotKey(IntPtr windowHandle, int hotkeyId)
        {
            KeyRegistrationApi.UnregisterHotKey(windowHandle, hotkeyId);
        }
    }
}