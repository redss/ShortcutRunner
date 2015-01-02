using System;
using ShortcutRunner.Properties;

namespace ShortcutRunner.ShortcutParsing
{
    public abstract class ShortcutParsingException : Exception
    {
        public string Shortcut { get; set; }
    }

    public class NoNonModifierKeysException : ShortcutParsingException
    {
        public override string Message
        {
            get { return string.Format(Resources.NoNonModifierKeysException, Shortcut); }
        }
    }

    public class KeyNotRecognizedException : ShortcutParsingException
    {
        public string NotRecognizedKey { get; set; }

        public override string Message
        {
            get { return string.Format(Resources.KeyNotRecognizedException, NotRecognizedKey, Shortcut); }
        }
    }

    public class MultipleNonModifierKeysException : ShortcutParsingException
    {
        public override string Message
        {
            get { return string.Format(Resources.MultipleNonModifierKeysException, Shortcut); }
        }
    }
}