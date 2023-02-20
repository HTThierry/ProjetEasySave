using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EasySave.lib.Services
{
    public class RunningProcess
    {
        /// <summary>
        /// Refonte : Fusion avec Overwatch
        /// </summary>

        private TaskCompletionSource<bool> eventHandled;
        
        public int CheckRunningProcess(string ProcessName)
        {
            Process[] process = Process.GetProcessesByName(ProcessName);
            if (process.Length > 0)
            {
                return 1;
            }
            return 0;
        }
    }
}