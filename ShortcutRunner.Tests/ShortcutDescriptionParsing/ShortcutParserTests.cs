using FakeItEasy;
using NUnit.Framework;
using ShortcutRunner.ShortcutDescriptionParsing;

namespace ShortcutRunner.Tests.ShortcutDescriptionParsing
{
    public class ShortcutParserTests
    {
        public ShortcutParser Sut = new ShortcutParser(A.Fake<IKeyParser>());

        [Test]
        public void Can_Parse_Shortcut_Description()
        {
            // Arrange

            var ctrlToken = A.Fake<IKeyToken>();
            var shiftToken = A.Fake<IKeyToken>();
            var f2Token = A.Fake<IKeyToken>();

            A.CallTo(() => Sut.KeyParser.Parse("Ctrl")).Returns(ctrlToken);
            A.CallTo(() => Sut.KeyParser.Parse("Shift")).Returns(shiftToken);
            A.CallTo(() => Sut.KeyParser.Parse("F2")).Returns(f2Token);

            // Act

            var result = Sut.Parse("  Ctrl +Shift+  F2");

            // Assert

            Assert.That(result, Is.EqualTo(new[] { ctrlToken, shiftToken, f2Token }));
        }
    }
}