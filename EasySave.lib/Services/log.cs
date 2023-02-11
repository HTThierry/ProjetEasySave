using System.Text.Json;
using System.Xml.Linq;


namespace EasySave.lib.Services
{
    public static class Log
    {
       public static void LogFiles(string[] LogArray)
        {
            if (LogArray != null)
            {
                Console.WriteLine(LogArray.GetType());
                Console.ReadKey();
                //DateTime today = DateTime.Now;
                //string day = today.ToString("MM/dd/yyyy");

                //var LogPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "EasySave.lib", "Log", "log.json");               //{day}_

                //string JsonLog = JsonSerializer.Serialize(LogArray);

                //File.AppendAllText(LogPath, JsonLog);                                                                                           // +Environment.NewLine

                //return 0;
            }
            //else
                //return 1;
        }
    }
}