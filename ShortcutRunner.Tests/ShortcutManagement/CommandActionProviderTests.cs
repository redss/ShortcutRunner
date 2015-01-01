using FakeItEasy;
using NUnit.Framework;
using ShortcutRunner.ShortcutManagement;

namespace ShortcutRunner.Tests.ShortcutManagement
{
    public class CommandActionProviderTests
    {
        [Test]
        public void Can_Create_Action_Running_A_Command()
        {
            // Arrange

            var sut = SutFactory.Create<CommandActionProvider>();
            var command = "a command";

            // Act

            var result = sut.CreateCommandAction(command);

            // Assert

            A.CallTo(() => sut.CommandRunner.RunCommand(command))
                .MustNotHaveHappened();

            result();

            A.CallTo(() => sut.CommandRunner.RunCommand(command))
                .MustHaveHappened();
        } 
    }
}