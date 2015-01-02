using System.Windows.Forms;
using ShortcutRunner.ShortcutManagement;

namespace ShortcutRunner.Presentation
{
    public interface IApplicationStarter
    {
        void Start();
    }

    public class ApplicationStarter : IApplicationStarter
    {
        public readonly IShortcutManager ShortcutManager;
        public readonly ITryIcon TryIcon;
        
        public ApplicationStarter(IShortcutManager shortcutManager, ITryIcon tryIcon)
        {
            ShortcutManager = shortcutManager;
            TryIcon = tryIcon;
        }

        public void Start()
        {
            ShortcutManager.Initialize();

            TryIcon.Initialize();
            TryIcon.OnExit += (sender, args) => Application.Exit();

            Application.Run();
        }
    }
}