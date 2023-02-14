using System.Diagnostics;

namespace EasySave.lib.Services
{
    public class RunningProcess
    {
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

        public bool RunningProcessClosed(string ProcessName)
        {
            Process process = new();
            process = Process.GetProcessesByName(ProcessName)[0];
            process.WaitForExit();
            return true;
        }
    }
}