using Microsoft.Practices.Unity;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoFakeItEasy;

namespace ShortcutRunner.Tests
{
    public class SutFactory
    {
        public static T Create<T>()
        {
            return new Fixture()
                .Customize(new AutoFakeItEasyCustomization())
                .Create<T>();
        }

        public static T CreateActual<T>() // todo: think of something else
        {
            return new UnityContainer()
                .ConfigureShortcutRunner()
                .Resolve<T>();
        }
    }
}