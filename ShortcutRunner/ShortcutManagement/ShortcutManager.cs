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
        public readonly ICommandRunner CommandRunner;

        public ShortcutManager(IConfigurationManager configurationManager, IShortcutController shortcutController, ICommandRunner commandRunner)
        {
            ConfigurationManager = configurationManager;
            ShortcutController = shortcutController;
            CommandRunner = commandRunner;
        }

        public void Initialize()
        {
            var configurationLines = ConfigurationManager.ReadConfigurationFile();

            foreach (var line in configurationLines)
            {
                var command = line.Command;
                Action action = () => CommandRunner.RunCommand(command);

                ShortcutController.RegisterShortcutAction(line.Shortcut, action);
            }
        }
    }
}