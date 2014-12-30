using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using NUnit.Framework;
using ShortcutRunner.ConfigurationParsing;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner.Tests.ConfigurationFileParsing
{
    class ConfigurationFileParserTests
    {
        public ConfigurationParser Sut = SutFactory.CreateActual<ConfigurationParser>();

        [Test]
        public void Can_Parse_Configuration_File()
        {
            // Arrange

            var configurationSource = new StringBuilder()
                .AppendLine("Ctrl + Shift + X -> some-command.bat --flag")
                .AppendLine("Alt + Shift + F -> accidental-arrow.bat ->")
                .AppendLine()
                .ToString();

            // Act

            var result = Sut.Parse(configurationSource);

            // Assert

            var expectedResult = new[]
            {
                new ConfigurationLine
                {
                    Shortcut = new ShortcutDescription(ModifierKeys.Ctrl | ModifierKeys.Shift, Keys.X),
                    Command = "some-command.bat --flag"
                },
                new ConfigurationLine
                {
                    Shortcut = new ShortcutDescription(ModifierKeys.Alt | ModifierKeys.Shift, Keys.F),
                    Command = "accidental-arrow.bat ->"
                }
            };

            Assert.That(result, Is.EqualTo(expectedResult)
                .Using(new ConfigurationLineEqualityComparer()));
        }

        [Test]
        public void Can_Handle_Invalid_Configuration()
        {
            var configurationSource = new StringBuilder()
                .AppendLine()
                .AppendLine("Ctrl + Shift + X -> some command")
                .AppendLine("some invalid line")
                .ToString();

            var exception = Assert.Throws<InvalidLineException>(() => 
                Sut.Parse(configurationSource));

            Assert.That(exception.InvalidLine, Is.EqualTo("some invalid line"));
            Assert.That(exception.LineNumber, Is.EqualTo(2));
        }
    }

    class ConfigurationLineEqualityComparer : IEqualityComparer<ConfigurationLine>
    {
        public bool Equals(ConfigurationLine x, ConfigurationLine y)
        {
            return x != null && y != null && x.Command == y.Command && x.Shortcut.Equals(y.Shortcut);
        }

        public int GetHashCode(ConfigurationLine obj)
        {
            return 0;
        }
    }
}