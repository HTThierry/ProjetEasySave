using System.Text.Json;
using System.Xml.Linq;


namespace EasySave.lib.Services
{
    public static class Log
    {
       public static int LogFiles(string[] LogArray)
        {
            if (LogArray != null)
            {
                DateTime today = DateTime.Now;
                string day = today.ToString("MM_dd_yyyy");

                var LogPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "EasySave.lib", "Log", $"{day}_log.json");

                string JsonLog = JsonSerializer.Serialize(LogArray);

                File.AppendAllText(LogPath, JsonLog + Environment.NewLine);

                return 0;
            }
            else
                return 1;
        }
    }
}