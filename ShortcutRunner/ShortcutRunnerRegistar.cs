using Microsoft.Practices.Unity;

namespace ShortcutRunner
{
    public static class ShortcutRunnerRegistar
    {
        public static IUnityContainer ConfigureShortcutRunner(this IUnityContainer container)
        {
            return container.RegisterTypes(
                AllClasses.FromAssemblies(typeof(Program).Assembly),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.ContainerControlled);
        }
    }
}