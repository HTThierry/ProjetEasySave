using System.Diagnostics;

namespace EasySave.lib.Services
{
    public class RunningProcess
    {
        public bool CheckRunningProcess(string ProcessName)
        {
            bool ProcessRunning = false;
            foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcesses())
            {
                if (process.ProcessName == ProcessName)
                {
                    ProcessRunning = true;
                }
            }
            return ProcessRunning;
        }

        public bool RunningProcessClosed(string ProcessName)
        {
            Process process = new();
            process.StartInfo.FileName = ProcessName;
            process.WaitForExit();
            return true;
        }
    }
}