using System.Collections.Generic;
using System.Linq;

namespace ShortcutRunner
{
    public interface IShortcutDescriptionParser
    {
        IEnumerable<IKeyToken> Parse(string shortcut);
    }

    public class ShortcutDescriptionParser : IShortcutDescriptionParser
    {
        private readonly IKeyParser _keyParser = new KeyParser();

        public IEnumerable<IKeyToken> Parse(string shortcut)
        {
            return GetShortcutParts(shortcut)
                .Select(_keyParser.Parse)
                .ToArray();
        }

        private IEnumerable<string> GetShortcutParts(string shortcut)
        {
            return shortcut.Split('+').Select(s => s.Trim());
        }
    }
}