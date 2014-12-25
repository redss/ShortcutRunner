using System.Linq;

namespace ShortcutRunner
{
    public interface IShortcutParser
    {
        IKeyToken[] Parse(string shortcut);
    }

    public class ShortcutParser : IShortcutParser
    {
        private readonly IKeyParser _keyParser = new KeyParser();

        public IKeyToken[] Parse(string shortcut)
        {
            return shortcut.Split('+')
                .Select(s => s.Trim())
                .Select(_keyParser.Parse)
                .ToArray();
        }
    }
}