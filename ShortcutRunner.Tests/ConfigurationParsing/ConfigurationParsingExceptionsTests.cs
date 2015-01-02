using System;
using NUnit.Framework;
using ShortcutRunner.ConfigurationParsing;

namespace ShortcutRunner.Tests.ConfigurationParsing
{
    class ConfigurationParsingExceptionsTests
    {
        [Test]
        public void Can_Format_InvalidConfigurationLineException_Message()
        {
            var sut = new InvalidConfigurationLineException
            {
                LineNumber = 10,
                InvalidLine = "an invalid line"
            };

            var expectedMessage =
                "Line 10 does not match shortcut description format:" + Environment.NewLine
               + "an invalid line";
            
            Assert.That(sut.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void Can_Format_InvalidShortcutInConfigurationException_Message()
        {
            var sut = new InvalidShortcutInConfigurationException
            {
                LineNumber = 12,
                InvalidLine = "invalid shortcut -> command",
                ShortcutException = new Exception("shortcut is invalid")
            };

            var expectedMessage = 
                "Shortcut at line 12 is invalid:" + Environment.NewLine
              + "invalid shortcut -> command" + Environment.NewLine
              + Environment.NewLine
              + "shortcut is invalid";

            Assert.That(sut.Message, Is.EqualTo(expectedMessage));
        }
    }
}