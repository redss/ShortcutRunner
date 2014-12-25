using System;

namespace ShortcutRunner.ShortcutDescriptionParsing
{
    public class ShortcutParsingException : Exception
    {
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