using System.Text.Json;

namespace EasySave.lib.Services
{
    public static class Log
    {
        public static int LogFiles(string[] LogArray)
        {
            if (LogArray != null)
            {
                List<LogEntry> logs = new List<LogEntry>();
                DateTime today = DateTime.Now;
                string day = today.ToString("MM_dd_yyyy");

                string LogPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "EasySave.lib", "Log", $"{day}_log.json");

                LogEntry LogEntry = new LogEntry
                {
                    Name = LogArray[0],
                    FileSource = LogArray[1],
                    FileTarget = LogArray[2],
                    destPath = LogArray[3],
                    FileSize = int.Parse(LogArray[4]),
                    FileTransferTime = double.Parse(LogArray[5].Replace('.', ',')),
                    time = DateTime.Parse(LogArray[6])
                };

                logs.Add(LogEntry);
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string JsonLog = JsonSerializer.Serialize(logs, options);

                File.AppendAllText(LogPath, JsonLog + Environment.NewLine);

                return 0;
            }
            else
                return 1;
        }
    }
}

public class LogEntry
{
    public string Name { get; set; }
    public string FileSource { get; set; }
    public string FileTarget { get; set; }
    public string destPath { get; set; }
    public int FileSize { get; set; }
    public double FileTransferTime { get; set; }
    public DateTime time { get; set; }
}