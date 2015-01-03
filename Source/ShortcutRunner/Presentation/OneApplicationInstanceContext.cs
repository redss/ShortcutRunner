using System;
using System.Threading;
using ShortcutRunner.Properties;

namespace ShortcutRunner.Presentation
{
    public class ApplicationAlreadyRunningException : Exception
    {
        public override string Message
        {
            get { return Resources.ApplicationAlreadyRunningException; }
        }
    }

    public class OneApplicationInstanceContext : IDisposable
    {
        private const string ApplicationGuid = "bc1c5beb-7ec8-4bf6-9d4c-f7090c54dcad";

        private readonly Mutex _mutex;

        public OneApplicationInstanceContext()
        {
            _mutex = new Mutex(initiallyOwned: false, name: "Global\\" + ApplicationGuid);

            var otherShortcutRunnerInstanceIsAlreadyRunning = !_mutex.WaitOne(0, false);

            if (otherShortcutRunnerInstanceIsAlreadyRunning)
            {
                throw new ApplicationAlreadyRunningException();
            }
        }

        public void Dispose()
        {
            _mutex.Dispose();
        }
    }
}