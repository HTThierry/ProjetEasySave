using System.Configuration;
using System.Diagnostics;

namespace EasySave.lib.Services
{
    public class cryptoSoft
    {
        public int cryptoSoftEasySave(string type, string fichier_source, string fichier_destination, string key)
        {
            ProcessStartInfo info = new ProcessStartInfo
            {
                FileName = $@"{ConfigurationManager.AppSettings["pathToCryptoSoft"]}\CryptoSoftEasySave.exe",
                Arguments = $"{type} {fichier_source} {fichier_destination} {key}",
            };

            Process process = new Process();
            process.StartInfo = info;
            process.Start();
            process.WaitForExit();
            var test = process.ExitCode;

            return (process.ExitCode);
        }
    }
}