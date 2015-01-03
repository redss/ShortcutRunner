using FakeItEasy;
using NUnit.Framework;
using ShortcutRunner.ConfigurationManagement;
using ShortcutRunner.ConfigurationParsing;

namespace ShortcutRunner.Tests.ConfigurationManagement
{
    class ConfigurationManagerTests
    {
        [Test]
        public void Can_Read_And_Parse_Configuration_File()
        {
            // Arrange

            var sut = SutFactory.Create<ConfigurationManager>();

            var configurationFile = "some content";

            A.CallTo(() => sut.FileReader.ReadFile(ConfigurationManager.ConfigurationFileName))
                .Returns(configurationFile);

            var parsedContent = new ConfigurationLine[] { };

            A.CallTo(() => sut.ConfigurationParser.Parse(configurationFile))
                .Returns(parsedContent);

            // Act

            var result = sut.ReadConfigurationFile();

            // Assert

            Assert.That(result, Is.SameAs(parsedContent));
        }
    }
}