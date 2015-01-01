using System;
using ShortcutRunner.CommandRunning;

namespace ShortcutRunner.ShortcutManagement
{
    public interface ICommandActionProvider
    {
        Action CreateCommandAction(string command);
    }

    public class CommandActionProvider : ICommandActionProvider
    {
        public readonly ICommandRunner CommandRunner;

        public CommandActionProvider(ICommandRunner commandRunner)
        {
            CommandRunner = commandRunner;
        }

        public Action CreateCommandAction(string command)
        {
            return () => CommandRunner.RunCommand(command);
        }
    }
}