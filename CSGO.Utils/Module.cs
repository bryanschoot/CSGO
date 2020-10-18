using System.Diagnostics;

namespace CSGO.Utils
{
    /// <summary>
    ///     Process and ProcessModule are combined
    /// </summary>
    public class Module
    {
        private Process Process { get; set; }
        private ProcessModule ProcessModule { get; set; }

        public Module(Process process, ProcessModule processModule)
        {
            Process = process;
            ProcessModule = processModule;
        }
    }
}
