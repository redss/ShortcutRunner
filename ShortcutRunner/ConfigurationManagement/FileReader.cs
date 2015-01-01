using System.IO;

namespace ShortcutRunner.ConfigurationManagement
{
    public interface IFileReader
    {
        string ReadFile(string path);
    }

    public class FileReader : IFileReader
    {
        public string ReadFile(string path)
        {
            return File.ReadAllText(path);
        }
    }
}