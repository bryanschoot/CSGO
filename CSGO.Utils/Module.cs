using System;
using System.Diagnostics;

namespace CSGO.Utils
{
    public class Module : IDisposable
    {
        /// <summary>
        ///     Defining processes and modules
        /// </summary>
        public Module(Process process, ProcessModule processModule)
        {
            Process = process;
            ProcessModule = processModule;
        }

        /// <inheritdoc cref="Process" />
        public Process Process { get; set; }

        /// <inheritdoc cref="ProcessModule" />
        public ProcessModule ProcessModule { get; set; }

        /// <inheritdoc cref="Dispose" />
        public void Dispose()
        {
            Process = default;

            ProcessModule.Dispose();
            ProcessModule = default;
        }
    }
}