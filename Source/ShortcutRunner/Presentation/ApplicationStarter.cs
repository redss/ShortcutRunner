using System;
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
        public readonly IErrorMessageDisplayer ErrorMessageDisplayer;
        
        public ApplicationStarter(IShortcutManager shortcutManager, ITryIcon tryIcon, IErrorMessageDisplayer errorMessageDisplayer)
        {
            ShortcutManager = shortcutManager;
            TryIcon = tryIcon;
            ErrorMessageDisplayer = errorMessageDisplayer;
        }

        public void Start()
        {
            try
            {
                StartIfNotAlreadyRunning();
            }
            catch (Exception e)
            {
                ErrorMessageDisplayer.DisplayErrorMessage(e.Message);
            }
        }

        private void StartIfNotAlreadyRunning()
        {
            using (new OneApplicationInstanceContext())
            {
                StartApplication();
            }
        }

        private void StartApplication()
        {
            ShortcutManager.Initialize();

            TryIcon.Initialize();
            TryIcon.OnExit += (sender, args) => Application.Exit();

            Application.Run();
        }
    }
}