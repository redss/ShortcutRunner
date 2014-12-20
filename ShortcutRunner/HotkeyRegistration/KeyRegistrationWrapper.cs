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
        private readonly IKeyRegistrationApi _keyRegistrationApi;

        public KeyRegistrationWrapper(IKeyRegistrationApi keyRegistrationApi)
        {
            _keyRegistrationApi = keyRegistrationApi;
        }

        public void RegisterHotKey(IntPtr windowHandle, int hotkeyId, ShortcutDescription shortcutDescription)
        {
            var registrationSucceeded = _keyRegistrationApi.RegisterHotKey(windowHandle, hotkeyId,
                (uint) shortcutDescription.Modifiers, (uint) shortcutDescription.Key);

            if (!registrationSucceeded)
            {
                throw new InvalidOperationException("Couldn’t register the hot key.");
            }
        }

        public void UnregisterHotKey(IntPtr windowHandle, int hotkeyId)
        {
            _keyRegistrationApi.UnregisterHotKey(windowHandle, hotkeyId);
        }
    }
}