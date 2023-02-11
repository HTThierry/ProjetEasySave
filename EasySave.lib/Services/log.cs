using System.Text.Json;
using System.Xml.Linq;


namespace EasySave.lib.Services
{
    public class log
    {
       public static int logFiles(string[] LogArray)
        {
            if (LogArray != null)
            {
                DateTime today = DateTime.Now;
                string day=today.ToString("MM/dd/yyyy");

                var logPath = Path.Combine("\\Services\\log\\", $"{ day}_log.json");

                string jsonLog = JsonSerializer.Serialize(LogArray);

                File.AppendAllText(logPath, jsonLog + Environment.NewLine);

                return 0;
            }
            else { return 1; }
        }
    }
}

