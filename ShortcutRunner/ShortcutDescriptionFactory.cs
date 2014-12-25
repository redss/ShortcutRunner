using System.Linq;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner
{
    public interface IShortcutDescriptionFactory
    {
        ShortcutDescription Create(IKeyToken[] keyTokens);
    }

    public class ShortcutDescriptionFactory : IShortcutDescriptionFactory
    {
        public ShortcutDescription Create(IKeyToken[] keyTokens)
        {
            var modifierKeys = keyTokens
                .OfType<ModifierKeyToken>()
                .Select(p => p.ModifierKeys)
                .Aggregate((a, b) => a | b);

            var key = keyTokens
                .OfType<KeyToken>()
                .Single()
                .Key;

            return new ShortcutDescription(modifierKeys, key);
        }
    }
}