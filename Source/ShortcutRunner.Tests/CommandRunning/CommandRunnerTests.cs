using NUnit.Framework;
using ShortcutRunner.CommandRunning;

namespace ShortcutRunner.Tests.CommandRunning
{
    class CommandRunnerTests
    {
        [Test]
        public void Can_Run_Command()
        {
            var sut = SutFactory.Create<CommandRunner>();

            var process = sut.RunCommand("exit 123");

            process.WaitForExit(1000);

            Assert.That(process.ExitCode, Is.EqualTo(123));
        }
    }
}