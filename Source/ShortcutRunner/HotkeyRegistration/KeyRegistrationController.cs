using System;
using System.Collections.Generic;

namespace ShortcutRunner.HotkeyRegistration
{
    public interface IKeyRegistrationController : IDisposable
    {
        void RegisterHotKey(IntPtr windowHandle, ShortcutDescription shortcutDescription);
    }

    public class KeyRegistrationController : IKeyRegistrationController
    {
        public readonly IKeyRegistrationWrapper KeyRegistrationWrapper;

        private IDictionary<int, IntPtr> _registeredHotkeys = new Dictionary<int, IntPtr>();
        private int _currentId;

        public KeyRegistrationController(IKeyRegistrationWrapper keyRegistrationWrapper)
        {
            KeyRegistrationWrapper = keyRegistrationWrapper;
        }

        public void RegisterHotKey(IntPtr windowHandle, ShortcutDescription shortcutDescription)
        {
            _currentId = _currentId + 1;
            _registeredHotkeys.Add(_currentId, windowHandle);

            KeyRegistrationWrapper.RegisterHotKey(windowHandle, _currentId, shortcutDescription);
        }

        public void Dispose()
        {
            foreach (var registeredHotkey in _registeredHotkeys)
            {
                KeyRegistrationWrapper.UnregisterHotKey(registeredHotkey.Value, registeredHotkey.Key);
            }
        }
    }
}