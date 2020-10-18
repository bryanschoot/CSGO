using System;
using System.Diagnostics;

namespace CSGO.Utils
{
    /// <summary>
    ///     Process and ProcessModule are combined
    /// </summary>
    public class Module
    {
        public Process Process { get; set; }
        public ProcessModule ProcessModule { get; set; }

        public Module(Process process, ProcessModule processModule)
        {
            Process = process;
            ProcessModule = processModule;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
