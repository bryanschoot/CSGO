using System;
using System.Threading;

namespace CSGO.Utils
{
    public abstract class Threaded : IDisposable
    {
        /// <summary>
        ///     Virtual member call in constructor.
        /// </summary>
        protected Threaded()
        {
            Thread = new Thread(ThreadStart)
            {
                // ReSharper disable once VirtualMemberCallInConstructor
                Name = ThreadName
            };
        }

        /// <summary>
        ///     Custom thread name.
        /// </summary>
        protected virtual string ThreadName => nameof(Threaded);

        /// <summary>
        ///     Timeout for thread to finish.
        /// </summary>
        protected virtual TimeSpan ThreadTimeout { get; set; } = new TimeSpan(0, 0, 0, 3);

        /// <summary>
        ///     Thread frame sleep.
        /// </summary>
        protected virtual TimeSpan ThreadFrameSleep { get; set; } = new TimeSpan(0, 0, 0, 0, 1);

        /// <summary>
        ///     Thread for this component.
        /// </summary>
        private Thread Thread { get; set; }

        /// <inheritdoc cref="Dispose" />
        public virtual void Dispose()
        {
            Thread.Interrupt();
            if (!Thread.Join(ThreadTimeout)) Thread.Abort();
            Thread = default;
        }

        /// <summary>
        ///     Launch thread for execute frames.
        /// </summary>
        public void Start()
        {
            Thread.Start();
        }

        /// <summary>
        ///     Thread method.
        /// </summary>
        private void ThreadStart()
        {
            try
            {
                while (true)
                {
                    FrameAction();
                    Thread.Sleep(ThreadFrameSleep);
                }
            }
            catch (ThreadInterruptedException)
            {
            }
        }

        /// <summary>
        ///     Frame to loop inside a thread.
        /// </summary>
        protected abstract void FrameAction();
    }
}