using System;
using System.Windows.Forms;
using ShortcutRunner.IoC;
using ShortcutRunner.Presentation;

namespace ShortcutRunner
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // TODO: Are they even needed?
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var container = ContainerProvider.CreateShortcutRunnerContriner())
            {
                container.AutoRegister();
                container.Resolve<IApplicationStarter>().Start();
            }
        }
    }
}
