using System;
using System.Windows.Forms;
using Microsoft.Practices.Unity;

namespace ShortcutRunner
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var container = new UnityContainer()
                .RegisterType<IKeyboardHook, KeyboardHook>();

            var form = container.Resolve<Form1>();

            Application.Run(form);
        }
    }
}
