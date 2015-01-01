using System;
using System.Windows.Forms;
using Microsoft.Practices.Unity;
using ShortcutRunner.Presentation;

namespace ShortcutRunner
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using(var container = new UnityContainer())
            {
                container
                    .ConfigureShortcutRunner()
                    .Resolve<ApplicationStarter>()
                    .Start();
            }
        }
    }
}
