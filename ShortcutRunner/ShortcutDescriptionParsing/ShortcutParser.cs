using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner.ShortcutDescriptionParsing
{
    public interface IShortcutParser
    {
        ShortcutDescription Create(string shortcut);
    }
    
    public class ShortcutParser : IShortcutParser
    {
        public readonly IKeyParser KeyParser;

        public ShortcutParser(IKeyParser keyParser)
        {
            KeyParser = keyParser;
        }

        public ShortcutDescription Create(string shortcut)
        {
            if (shortcut == null)
            {
                throw new ArgumentNullException("shortcut");
            }

            try
            {
                return CreateShortcut(shortcut);
            }
            catch (ShortcutParsingException e)
            {
                e.Shortcut = shortcut;
                throw;
            }
        }

        private ShortcutDescription CreateShortcut(string shortcut)
        {
            var tokens = SplitToTokens(shortcut);

            return new ShortcutDescription(
                GetModifierKeys(tokens),
                GetKey(tokens));
        }

        private IKeyToken[] SplitToTokens(string shortcut)
        {
            return shortcut.Split('+')
                .Select(s => s.Trim())
                .Select(KeyParser.Parse)
                .ToArray();
        }

        private ModifierKeys GetModifierKeys(IEnumerable<IKeyToken> tokens)
        {
            return tokens
                .OfType<ModifierKeyToken>()
                .Select(p => p.ModifierKeys)
                .Aggregate(ModifierKeys.None, (a, b) => a | b);
        }

        private Keys GetKey(IEnumerable<IKeyToken> tokens)
        {
            var keyTokens = tokens
                .OfType<KeyToken>()
                .ToArray();

            if (keyTokens.Length > 1)
            {
                throw new MultipleNonModifierKeysException();
            }

            if (keyTokens.Length == 0)
            {
                throw new NoNonModifierKeysException();
            }

            return keyTokens
                .Single()
                .Key;
        }
    }
}