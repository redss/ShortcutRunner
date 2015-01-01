using System.Diagnostics;

namespace ShortcutRunner.CommandRunning
{
    public interface ICommandRunner
    {
        Process RunCommand(string command);
    }

    public class CommandRunner : ICommandRunner
    {
        public Process RunCommand(string command)
        {
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = "/C " + command // /C Carries out the command specified by string and then terminates
            };

            var process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();

            return process;
        }
    }
}