using TinyIoC;

namespace ShortcutRunner.IoC
{
    public static class ContainerProvider
    {
        public static TinyIoCContainer CreateShortcutRunnerContriner()
        {
            var container = new TinyIoCContainer();

            container.AutoRegister();

            return container;
        }
    }
}