namespace EasySave.lib.Services
{
    public class LogModel
    {
            public string Name { get; set; }
            public string FileSource { get; set; }
            public string FileTarget { get; set; }
            public string destPath { get; set; }
            public int FileSize { get; set; }
            public double FileTransferTime { get; set; }
            public DateTime time { get; set; }
    }
}