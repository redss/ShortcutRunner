using TinyIoC;

namespace ShortcutRunner.Container
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