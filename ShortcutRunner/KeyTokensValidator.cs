using System.Linq;

namespace ShortcutRunner
{
    public interface IKeyTokensValidator
    {
        void Validate(IKeyToken[] keyTokens);
    }

    public class KeyTokensValidator : IKeyTokensValidator
    {
        public void Validate(IKeyToken[] keyTokens)
        {
            if (keyTokens.OfType<KeyToken>().Count() > 1)
            {
                throw new MultipleNonModifierKeysException();
            }

            if (!keyTokens.OfType<KeyToken>().Any())
            {
                throw new NoNonModifierKeysException();
            }
        }
    }
}