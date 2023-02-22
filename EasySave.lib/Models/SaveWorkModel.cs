namespace EasySave.lib.Models
{
    public class SaveWorkModel
    {
        public string NameSaveWork { get; set; }

        public int TypeSaveWork { get; set; }

        public string SourcePathSaveWork { get; set; }

        public string DestinationPathSaveWork { get; set; }

        public ProgressStateModel ProgressStateModel { get; set; } = new ProgressStateModel();
        
        public AutoResetEvent PauseEvent = new AutoResetEvent(true);
    }
}