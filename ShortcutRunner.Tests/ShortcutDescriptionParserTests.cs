﻿using System;
using System.Windows.Forms;
using NUnit.Framework;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner.Tests
{
    public class ShortcutDescriptionParserTests
    {
        public ShortcutDescriptionParser Sut = new ShortcutDescriptionParser();

        public class ParseTestCase
        {
            public string ShortcutText { get; set; }
            public ShortcutDescription ExpectedResult { get; set; }
        }

        [Datapoints]
        public ParseTestCase[] TestCases =
        {
            new ParseTestCase
            {
                ShortcutText = "Ctrl + Shift + C",
                ExpectedResult = new ShortcutDescription(ModifierKeys.Ctrl | ModifierKeys.Shift, Keys.C)
            },
            new ParseTestCase
            {
                ShortcutText = "Alt + Shift + F12",
                ExpectedResult = new ShortcutDescription(ModifierKeys.Alt | ModifierKeys.Shift, Keys.F12)
            }
        };

        [TestCaseSource("TestCases")]
        public void Can_Parse_Shortcut_Description(ParseTestCase testCase)
        {
            var result = Sut.Parse(testCase.ShortcutText);

            Assert.That(result, Is.EqualTo(testCase.ExpectedResult));
        }

        [Test]
        public void Throws_Exception_When_Non_Modifier_Keys_Are_Used_More_Than_Once()
        {
            Assert.That(() => Sut.Parse("Ctrl + A + B"),
                Throws.TypeOf<MultipleNonModifierKeysException>());
        }

        [Test]
        public void Throws_Exception_When_Key_Is_Invalid()
        {
            Assert.That(() => Sut.Parse("Ctrl + Invalid Key"),
                Throws.TypeOf<KeyNotRecognizedException>()); // todo check invalid key string
        }

        [Test]
        public void Throws_Exception_When_Input_Is_Null()
        {
            Assert.That(() => Sut.Parse(null),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Throws_Exception_When_There_Is_No_Non_Modifier_Key()
        {
            Assert.That(() => Sut.Parse("Ctrl + Shift"),
                Throws.TypeOf<NoNonModifierKeysException>());
        }
    }
}