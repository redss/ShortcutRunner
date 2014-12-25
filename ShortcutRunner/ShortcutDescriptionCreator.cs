using System;
using System.Linq;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner
{
    public interface IShortcutDescriptionCreator
    {
        ShortcutDescription Create(string shortcut);
    }
    
    public class ShortcutDescriptionCreator : IShortcutDescriptionCreator
    {
        public readonly IShortcutParser Parser = new ShortcutParser(new KeyParser());

        public ShortcutDescription Create(string shortcut)
        {
            if (shortcut == null)
            {
                throw new ArgumentNullException("shortcut");
            }

            var parts = Parser.Parse(shortcut).ToArray();

            if (!parts.OfType<KeyToken>().Any())
            {
                throw new NoNonModifierKeysException();
            }

            if (parts.OfType<KeyToken>().Count() > 1)
            {
                throw new MultipleNonModifierKeysException();
            }

            var modifierKeys = parts
                .OfType<ModifierKeyToken>()
                .Select(p => p.ModifierKeys)
                .Aggregate((a, b) => a | b);

            var key = parts
                .OfType<KeyToken>()
                .Single()
                .Key;

            return new ShortcutDescription(modifierKeys, key);
        }
    }
}