using System.Linq;

namespace ShortcutRunner.ShortcutDescriptionParsing
{
    public interface IShortcutParser
    {
        IKeyToken[] Parse(string shortcut);
    }

    public class ShortcutParser : IShortcutParser
    {
        public readonly IKeyParser KeyParser;

        public ShortcutParser(IKeyParser keyParser)
        {
            KeyParser = keyParser;
        }

        public IKeyToken[] Parse(string shortcut)
        {
            return shortcut.Split('+')
                .Select(s => s.Trim())
                .Select(KeyParser.Parse)
                .ToArray();
        }
    }
}