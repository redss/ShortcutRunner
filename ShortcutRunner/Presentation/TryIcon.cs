using System;
using System.Windows.Forms;
using ShortcutRunner.Properties;

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
                Text = Resources.TryIconExitMenuItem
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
                Text = Resources.TryIconTitle,
                Visible = true,
                Icon = new Form().Icon, // TODO: This obviously have to be changed.
                BalloonTipTitle = Resources.BalloonTipTitle,
                BalloonTipText = Resources.BalloonTipText
            };

            _notifyIcon.ShowBalloonTip(1000 * 5);
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