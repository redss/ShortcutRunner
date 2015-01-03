using System;
using ShortcutRunner.CommandRunning;
using ShortcutRunner.ConfigurationManagement;

namespace ShortcutRunner.ShortcutManagement
{
    public interface IShortcutManager
    {
        void Initialize();
    }

    public class ShortcutManager : IShortcutManager
    {
        public readonly IConfigurationManager ConfigurationManager;
        public readonly IShortcutController ShortcutController;
        public readonly ICommandActionProvider CommandActionProvider;

        public ShortcutManager(IConfigurationManager configurationManager, IShortcutController shortcutController, ICommandActionProvider commandActionProvider)
        {
            ConfigurationManager = configurationManager;
            ShortcutController = shortcutController;
            CommandActionProvider = commandActionProvider;
        }

        public void Initialize()
        {
            var configurationLines = ConfigurationManager.ReadConfigurationFile();

            foreach (var line in configurationLines)
            {
                var action = CommandActionProvider.CreateCommandAction(line.Command);

                ShortcutController.RegisterShortcutAction(line.Shortcut, action);
            }
        }
    }
}