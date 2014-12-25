using System;
using System.Windows.Forms;
using Microsoft.Practices.Unity;
using ShortcutRunner.HotkeyRegistration;

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
                .RegisterType<IKeyboardHook, KeyboardHook>()
                .RegisterType<IShortcutDescriptionCreator, ShortcutDescriptionCreator>()
                .RegisterType<IKeyRegistrationApi, KeyRegistrationApi>()
                .RegisterType<IMessageCatchingWindow, MessageCatchingWindow>()
                .RegisterType<IKeyRegistrationWrapper, KeyRegistrationWrapper>();

            var form = container.Resolve<Form1>();

            Application.Run(form);
        }
    }
}
