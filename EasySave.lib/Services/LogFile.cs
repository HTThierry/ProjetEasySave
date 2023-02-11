using EasySave.lib.Models;
using System.Text.Json;

namespace ProjetEasySave.lib.Functions
{
    public class LogFile
    {
        public LogFileModel _LogFileModel = new();
        public SaveWorkModel _SaveWorkModel = new();

        public void log()
        {
            string path = _SaveWorkModel.SourcePathSaveWork;
            _LogFileModel.Name = Path.GetFileName(path);

            DateTime today = DateTime.Now;

            FileInfo fileInfo = new FileInfo(_SaveWorkModel.SourcePathSaveWork);

            var LogStructure = new
            {
                Name = _LogFileModel.Name,
                FileSource = _SaveWorkModel.SourcePathSaveWork,
                FileTarget = _SaveWorkModel.DestinationPathSaveWork,
                DestPath = "",
                FileSize = fileInfo.Length,
                FileTransferTime = _LogFileModel.Timer(),
                Time = today.ToString("MM/dd/yyyy hh:mm:ss"),
            };
            string jsonLog = JsonSerializer.Serialize(LogStructure);

            if (!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "EasySave.lib", "Services", "SaveWorks")))
            {
                File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "EasySave.lib", "Services", "SaveWorks"), jsonLog);
            }
            else
            {
                File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "EasySave.lib", "Services", "SaveWorks"), jsonLog);
            }
        }
    }
}