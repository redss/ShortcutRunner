using System;

namespace ShortcutRunner.ShortcutParsing
{
    public class ShortcutParsingException : Exception
    {
        public string Shortcut { get; set; }
    }

    public class NoNonModifierKeysException : ShortcutParsingException
    {
    }

    public class KeyNotRecognizedException : ShortcutParsingException
    {
        public string NotRecognizedKey { get; set; }
    }

    public class MultipleNonModifierKeysException : ShortcutParsingException
    {
    }
}