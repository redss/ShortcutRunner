using System;

namespace ShortcutRunner.ShortcutDescriptionParsing
{
    // TODO: More detailed parsing information.

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