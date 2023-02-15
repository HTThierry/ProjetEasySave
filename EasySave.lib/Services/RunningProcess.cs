using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EasySave.lib.Services
{
    public class RunningProcess
    {
        private TaskCompletionSource<bool> eventHandled;
        
        public bool CheckRunningProcess(string ProcessName)
        {
            bool ProcessRunning = false;
            var process = Process.GetProcessesByName(ProcessName);
            if (process.Length > 0)
            {
                ProcessRunning = true;
            }
            return ProcessRunning;
        }
    }
}