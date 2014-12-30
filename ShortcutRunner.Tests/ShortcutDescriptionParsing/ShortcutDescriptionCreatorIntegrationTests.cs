using System.Windows.Forms;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using ShortcutRunner.HotkeyRegistration;
using ShortcutRunner.ShortcutDescriptionParsing;

namespace ShortcutRunner.Tests.ShortcutDescriptionParsing
{
    public class ShortcutDescriptionCreatorIntegrationTests
    {
        public ShortcutDescriptionCreator Sut = SutFactory.CreateActual<ShortcutDescriptionCreator>();

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
            var result = Sut.Create(testCase.ShortcutText);

            Assert.That(result, Is.EqualTo(testCase.ExpectedResult));
        }
    }
}