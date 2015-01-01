using ShortcutRunner.ConfigurationParsing;

namespace ShortcutRunner.ConfigurationManagement
{
    public interface IConfigurationManager
    {
        ConfigurationLine[] ReadConfigurationFile();
    }

    public class ConfigurationManager : IConfigurationManager
    {
        public const string ConfigurationFileName = "shortcuts.txt";

        public readonly IFileReader FileReader;
        public readonly IConfigurationParser ConfigurationParser;

        public ConfigurationManager(IFileReader fileReader, IConfigurationParser configurationParser)
        {
            FileReader = fileReader;
            ConfigurationParser = configurationParser;
        }

        public ConfigurationLine[] ReadConfigurationFile()
        {
            var content = FileReader.ReadFile(ConfigurationFileName);

            return ConfigurationParser.Parse(content);
        }
    }
}