using System;
using System.Windows.Forms;

namespace ShortcutRunner.Presentation
{
    public interface ITryIcon : IDisposable
    {
        event EventHandler OnExit;
        void Initialize();
    }

    public class TryIcon : ITryIcon
    {
        public event EventHandler OnExit;

        private NotifyIcon _notifyIcon;

        public void Initialize()
        {
            var closeButton = new MenuItem
            {
                Index = 0,
                Text = "Exit"
            };

            closeButton.Click += (sender, args) =>
            {
                closeButton.Visible = false;
                OnExit(this, args);
            };

            var contextMenu = new ContextMenu();

            contextMenu.MenuItems.Add(closeButton);

            _notifyIcon = new NotifyIcon
            {
                ContextMenu = contextMenu,
                Text = "Shortcut Runner",
                Visible = true,
                Icon = new Form().Icon // TODO: This obviously have to be changed.
            };
        }

        public void Dispose()
        {
            if (_notifyIcon != null)
            {
                _notifyIcon.Dispose();
            }
        }
    }
}