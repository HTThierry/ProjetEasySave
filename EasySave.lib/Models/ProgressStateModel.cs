namespace EasySave.lib.Models
{
    public class ProgressStateModel
    { //changer les strings par des types précis
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public string ProgressState { get; set; }
        public int TotalFilesToCopy { get; set; }
        public long TotalFilesSizeToCopy { get; set; }
        public int NbFilesLeft { get; set; }
        public long FilesSizeLeft { get; set; }
        public string FilePath { get; set; }
        public string FileDestinationPath { get; set; }
    }
}