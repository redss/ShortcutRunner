using System;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner.ShortcutDescriptionParsing
{
    public interface IShortcutDescriptionCreator
    {
        ShortcutDescription Create(string shortcut);
    }
    
    public class ShortcutDescriptionCreator : IShortcutDescriptionCreator
    {
        public readonly IShortcutParser ShortcutParser;
        public readonly IKeyTokensValidator KeyTokensValidator;
        public readonly IShortcutDescriptionFactory ShortcutDescriptionFactory;

        public ShortcutDescriptionCreator(IShortcutParser shortcutParser, IKeyTokensValidator keyTokensValidator, IShortcutDescriptionFactory shortcutDescriptionFactory)
        {
            ShortcutParser = shortcutParser;
            KeyTokensValidator = keyTokensValidator;
            ShortcutDescriptionFactory = shortcutDescriptionFactory;
        }

        public ShortcutDescription Create(string shortcut)
        {
            if (shortcut == null)
            {
                throw new ArgumentNullException("shortcut");
            }

            var tokens = ShortcutParser.Parse(shortcut);

            KeyTokensValidator.Validate(tokens);

            return ShortcutDescriptionFactory.Create(tokens);
        }
    }
}