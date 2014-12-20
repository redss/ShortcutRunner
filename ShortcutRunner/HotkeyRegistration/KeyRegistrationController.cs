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
        private readonly IKeyRegistrationWrapper _keyRegistrationWrapper;

        private IDictionary<int, IntPtr> _registeredHotkeys = new Dictionary<int, IntPtr>();
        private int _currentId;

        public KeyRegistrationController(IKeyRegistrationWrapper keyRegistrationWrapper)
        {
            _keyRegistrationWrapper = keyRegistrationWrapper;
        }

        public void RegisterHotKey(IntPtr windowHandle, ShortcutDescription shortcutDescription)
        {
            _currentId = _currentId + 1;
            _registeredHotkeys.Add(_currentId, windowHandle);

            _keyRegistrationWrapper.RegisterHotKey(windowHandle, _currentId, shortcutDescription);
        }

        public void Dispose()
        {
            foreach (var registeredHotkey in _registeredHotkeys)
            {
                _keyRegistrationWrapper.UnregisterHotKey(registeredHotkey.Value, registeredHotkey.Key);
            }
        }
    }
}