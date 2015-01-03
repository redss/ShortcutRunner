using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using NUnit.Framework;
using ShortcutRunner.ConfigurationParsing;
using ShortcutRunner.HotkeyRegistration;
using ShortcutRunner.ShortcutParsing;

namespace ShortcutRunner.Tests.ConfigurationParsing
{
    class ConfigurationParserTests
    {
        public ConfigurationParser Sut;

        [SetUp]
        public void SetUp()
        {
            // TODO: Use mocks instead?
            Sut = new ConfigurationParser(new ShortcutParser(new KeyParser()));
        }

        [Test]
        public void Can_Parse_Configuration_File()
        {
            // Arrange

            var configurationSource = new StringBuilder()
                .AppendLine("# This is comment")
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
                .AppendLine("Ctrl + Shift + X -> some command")
                .AppendLine("some invalid line")
                .ToString();

            var exception = Assert.Throws<InvalidConfigurationLineException>(() => 
                Sut.Parse(configurationSource));

            Assert.That(exception.InvalidLine, Is.EqualTo("some invalid line"));
            Assert.That(exception.LineNumber, Is.EqualTo(2));
        }

        [Test]
        public void Can_Handle_Invalid_Shortcut()
        {
            var configurationSource = new StringBuilder()
                .AppendLine("Ctrl + Shift + X -> some command")
                .AppendLine("invalid shortcut -> some command")
                .ToString();

            var exception = Assert.Throws<InvalidShortcutInConfigurationException>(() =>
                Sut.Parse(configurationSource));

            Assert.That(exception.InvalidLine, Is.EqualTo("invalid shortcut -> some command"));
            Assert.That(exception.LineNumber, Is.EqualTo(2));
            Assert.That(exception.ShortcutException, Is.InstanceOf<ShortcutParsingException>());
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