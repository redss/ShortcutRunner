using System;

namespace ShortcutRunner.ShortcutParsing
{
    public abstract class ShortcutParsingException : Exception
    {
        public string Shortcut { get; set; }
    }

    // TODO: Use resources.

    public class NoNonModifierKeysException : ShortcutParsingException
    {
        public override string Message
        {
            get { return string.Format("Shortcut '{0}' contains no non-modifier key.", Shortcut); }
        }
    }

    public class KeyNotRecognizedException : ShortcutParsingException
    {
        public string NotRecognizedKey { get; set; }

        public override string Message
        {
            get { return string.Format("Key '{0}' was not recognized in '{1}' shortcut.", NotRecognizedKey, Shortcut); }
        }
    }

    public class MultipleNonModifierKeysException : ShortcutParsingException
    {
        public override string Message
        {
            get { return string.Format("Shortcut '{0}' conatins more than one non-modifier key.", Shortcut); }
        }
    }
}