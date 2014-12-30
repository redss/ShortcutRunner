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
        [Test]
        public void Can_Parse_Configuration_File()
        {
            // Arrange

            var configurationSource = new StringBuilder();

            configurationSource.AppendLine("Ctrl + Shift + X -> some-command.bat --flag");
            configurationSource.AppendLine("Alt + Shift + F -> other-command.bat");
            configurationSource.AppendLine();

            var sut = SutFactory.CreateActual<ConfigurationParser>();

            // Act

            var result = sut.Parse(configurationSource.ToString());

            // Assert

            var expectedResult = new[]
            {
                new ConfigurationLine
                {
                    Shortcut = new ShortcutDescription(ModifierKeys.Ctrl | ModifierKeys.Shift, Keys.X),
                    Command = " some-command.bat --flag"
                },
                new ConfigurationLine
                {
                    Shortcut = new ShortcutDescription(ModifierKeys.Alt | ModifierKeys.Shift, Keys.F),
                    Command = " other-command.bat"
                }
            };

            Assert.That(result, Is.EqualTo(expectedResult)
                .Using(new ConfigurationLineEqualityComparer()));
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