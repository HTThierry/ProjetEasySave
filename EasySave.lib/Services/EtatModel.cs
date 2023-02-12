namespace EasySave.lib.Services
{
    public class EtatModel
    {
        public string Name { get; set; }
        public string Time { get; set; }
        public string CurrentState { get; set; }
        public string TotalFilesToCopy { get; set; }
        public string TotalFilesSizeToCopy { get; set; }
        public string NbFilesLeft { get; set; }
        public string FilesSizeLeft { get; set; }
        public string FilePath { get; set; }
        public string FileDestinationPath { get; set; }
    }
}