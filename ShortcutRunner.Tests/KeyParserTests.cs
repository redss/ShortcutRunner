using System.Windows.Forms;
using NUnit.Framework;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner.Tests
{
    public class KeyParserTests
    {
        public KeyParser Sut = new KeyParser();

        public class KeyParserTestCase
        {
            public string Key { get; set; }
            public IKeyToken Token { get; set; }
        };

        [Datapoints]
        public KeyParserTestCase[] TestCases =
        {
            new KeyParserTestCase { Key = "Ctrl", Token = new ModifierKeyToken(ModifierKeys.Ctrl) },
            new KeyParserTestCase { Key = "Alt", Token = new ModifierKeyToken(ModifierKeys.Alt) },
            new KeyParserTestCase { Key = "Shift", Token = new ModifierKeyToken(ModifierKeys.Shift) },
            
            new KeyParserTestCase { Key = "K", Token = new KeyToken(Keys.K) },
            new KeyParserTestCase { Key = "F5", Token = new KeyToken(Keys.F5) }
        };

        [Theory]
        public void Can_Parse_Key(KeyParserTestCase testCase)
        {
            var result = Sut.Parse(testCase.Key);

            Assert.That(result, Is.EqualTo(testCase.Token));
        }
    }
}