using EasySave.lib.Models;
using System.Text.Json;
using System.Configuration;

namespace EasySave.lib.Services
{
    public static class Log
    {
        public static int LogFiles(string[] LogArray)
        {
            if (LogArray != null)
            {
                List<LogModel> logs = new List<LogModel>();
                DateTime today = DateTime.Now;
                string day = today.ToString("dd_MM_yyyy");

                string DirectoryPath = ConfigurationManager.AppSettings["LogPath"];
                string LogPath = Path.Combine(DirectoryPath, $"{day}_log.json");

                LogModel _LogModel = new LogModel
                {
                    Name = LogArray[0],
                    FileSource = LogArray[1],
                    FileTarget = LogArray[2],
                    destPath = LogArray[3],
                    FileSize = int.Parse(LogArray[4]),
                    FileTransferTime = double.Parse(LogArray[5].Replace('.', ',')),
                    time = DateTime.Parse(LogArray[6])
                };

                logs.Add(_LogModel);

                if (!Directory.Exists(DirectoryPath))
                {
                    Directory.CreateDirectory(DirectoryPath);
                }

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string JsonLog = JsonSerializer.Serialize(logs, options);

                try
                {
                    File.AppendAllText(LogPath, JsonLog + Environment.NewLine);
                }
                catch
                {
                    return 1;
                }

                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}