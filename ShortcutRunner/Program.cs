using System;
using System.Windows.Forms;
using ShortcutRunner.Presentation;
using TinyIoC;

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

            using (var container = new TinyIoCContainer())
            {
                container.AutoRegister();
                container.Resolve<IApplicationStarter>().Start();
            }
        }
    }
}
