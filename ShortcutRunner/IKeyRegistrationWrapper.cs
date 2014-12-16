using System;

namespace ShortcutRunner
{
    public interface IKeyRegistrationWrapper
    {
        void RegisterHotKey(IntPtr hWnd, int id, ShortcutDescription shortcutDescription);
        void UnregisterHotKey(IntPtr hWnd, int id);
    }

    public class KeyRegistrationWrapper : IKeyRegistrationWrapper
    {
        private readonly IKeyRegistrationApi _keyRegistrationApi;

        public KeyRegistrationWrapper(IKeyRegistrationApi keyRegistrationApi)
        {
            _keyRegistrationApi = keyRegistrationApi;
        }

        public void RegisterHotKey(IntPtr hWnd, int id, ShortcutDescription shortcutDescription)
        {
            var registrationResult = _keyRegistrationApi.RegisterHotKey(hWnd, id, 
                (uint) shortcutDescription.Modifiers, (uint) shortcutDescription.Key);

            if (registrationResult == false)
            {
                throw new InvalidOperationException("Couldn’t register the hot key.");
            }
        }

        public void UnregisterHotKey(IntPtr hWnd, int id)
        {
            _keyRegistrationApi.UnregisterHotKey(hWnd, id);
        }
    }
}