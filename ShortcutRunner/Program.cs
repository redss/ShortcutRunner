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

            var container = new UnityContainer();

            container.RegisterTypes(
                AllClasses.FromAssemblies(typeof (Program).Assembly),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.ContainerControlled);

            var form = container.Resolve<Form1>();

            form.Disposed += (sender, args) => container.Dispose();

            Application.Run(form);
        }
    }
}
