using System;
using System.Runtime.InteropServices;

namespace ShortcutRunner
{
    public interface IKeyRegistrationApi
    {
        bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        bool UnregisterHotKey(IntPtr hWnd, int id);
    }

    public class KeyRegistrationApi : IKeyRegistrationApi
    {
        [DllImport("user32.dll", EntryPoint = "RegisterHotKey")]
        private static extern bool RegisterHotKeyImport(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll", EntryPoint = "UnregisterHotKey")]
        private static extern bool UnregisterHotKeyImport(IntPtr hWnd, int id);

        public bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk)
        {
            return RegisterHotKeyImport(hWnd, id, fsModifiers, vk);
        }

        public bool UnregisterHotKey(IntPtr hWnd, int id)
        {
            return UnregisterHotKeyImport(hWnd, id);
        }
    }
}