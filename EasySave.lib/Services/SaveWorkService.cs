using EasySave.lib.Models;
using System.Configuration;
using System.Diagnostics;

namespace EasySave.lib.Services
{
    public class SaveWorkService
    {
        private bool TokenOfPriority { get; set; } = true;
        private static bool TokenOfAvailability { get; set; } = true;

        //public SaveWorkModel _SaveWorkModel { get; set; } = new SaveWorkModel();
        //public cryptoSoft _cryptoSoft = new cryptoSoft();
        //public RunningProcess _RunningProcess = new RunningProcess();

        //public string[] GetInstanceInfo()
        //{
        //    string[] AttributsForPresentation = new string[4] { _SaveWorkModel.NameSaveWork, $"{_SaveWorkModel.TypeSaveWork}", _SaveWorkModel.SourcePathSaveWork, _SaveWorkModel.DestinationPathSaveWork };
        //    return AttributsForPresentation;
        //}

        public int LaunchSaveWork(SaveWorkModel model)
        {
            if (model.TypeSaveWork == 1)
            {
                return CompleteCopyFiles(model);
            }
            else
            {
                return DifferentialCopyFiles(model);
            }
        }

        private int CompleteCopyFiles(SaveWorkModel model)
        {
            CopyModel copyModel = new CopyModel()
            {
                SourcePath = model.SourcePathSaveWork,
                DestinationPath = model.DestinationPathSaveWork,
                TotalFilesToCopy = 0,
                TotalFilesSizeToCopy = 0,
                NbFilesLeft = 0,
                FilesSizeLeft = 0,
                Percentage = 0,
            };

            if (Directory.Exists(copyModel.SourcePath))
            {
                Directory.CreateDirectory(copyModel.DestinationPath);

                foreach (string dirPath in Directory.GetDirectories(copyModel.SourcePath, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(copyModel.SourcePath, copyModel.DestinationPath));
                }

                string[] AllFiles = Directory.GetFiles(copyModel.SourcePath, "*.*", SearchOption.AllDirectories);

                string[] SpecificExtensions = ConfigurationManager.AppSettings["FileToPrioritize"].Split(',');
                long FileSizeLimit = long.Parse(ConfigurationManager.AppSettings["FileSizeLimit"]);

                string[] PrioritizedFiles = AllFiles.Where(file => SpecificExtensions.Contains(Path.GetExtension(file))).ToArray();
                string[] OtherFiles = AllFiles.Except(PrioritizedFiles).ToArray();

                // Création des 4 listes
                string[] PrioritizedBigFiles = PrioritizedFiles.Where(file => new FileInfo(file).Length <= FileSizeLimit).ToArray();
                string[] PrioritizedSmallFiles = PrioritizedFiles.Except(PrioritizedFiles).ToArray();

                string[] OtherBigFiles = OtherFiles.Where(file => new FileInfo(file).Length >= FileSizeLimit).ToArray();
                string[] OtherSmallFiles = OtherFiles.Except(OtherBigFiles).ToArray();

                // Création des 4 index

                int indexA = 0;
                int indexB = 0;
                int indexC = 0;
                int indexD = 0;


                copyModel.TotalFilesToCopy = AllFiles.Length;
                copyModel.NbFilesLeft = copyModel.TotalFilesToCopy;

                foreach (string file in AllFiles)
                {
                    copyModel.TotalFilesSizeToCopy += new FileInfo(file).Length;
                }
                copyModel.FilesSizeLeft = copyModel.TotalFilesSizeToCopy;

                CompleteCopyListOfFiles(PrioritizedFiles, model, copyModel);
                CompleteCopyListOfFiles(OtherFiles, model, copyModel);
            }
            model.ProgressStateModel.Name = model.NameSaveWork;
            model.ProgressStateModel.Time = DateTime.Now;
            model.ProgressStateModel.ProgressState = "Inactive";
            model.ProgressStateModel.TotalFilesToCopy = 0;
            model.ProgressStateModel.TotalFilesSizeToCopy = 0;
            model.ProgressStateModel.NbFilesLeft = 0;
            model.ProgressStateModel.FilesSizeLeft = 0;
            model.ProgressStateModel.Percentage = 0;
            model.ProgressStateModel.FilePath = "";
            model.ProgressStateModel.FileDestinationPath = "";

            ProgressStateService.ProgressStateFile();
            return 0;
        }

        private int DifferentialCopyFiles(SaveWorkModel model)
        {
            CopyModel copyModel = new CopyModel()
            {
                SourcePath = model.SourcePathSaveWork,
                DestinationPath = model.DestinationPathSaveWork,
                TotalFilesToCopy = 0,
                TotalFilesSizeToCopy = 0,
                NbFilesLeft = 0,
                FilesSizeLeft = 0,
                Percentage = 0,
            };

            if (Directory.Exists(copyModel.SourcePath))
            {
                Directory.CreateDirectory(copyModel.DestinationPath);

                foreach (string dirPath in Directory.GetDirectories(copyModel.SourcePath, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(copyModel.SourcePath, copyModel.DestinationPath));
                }

                string[] AllFiles = Directory.GetFiles(copyModel.SourcePath, "*.*", SearchOption.AllDirectories);

                string[] SpecificExtensions = ConfigurationManager.AppSettings["FileToPrioritize"].Split(',');
                string[] PrioritizedFiles = AllFiles.Where(file => SpecificExtensions.Contains(Path.GetExtension(file))).ToArray();
                string[] OtherFiles = AllFiles.Except(PrioritizedFiles).ToArray();

                copyModel.TotalFilesToCopy = AllFiles.Length;
                copyModel.NbFilesLeft = copyModel.TotalFilesToCopy;

                foreach (string file in AllFiles)
                {
                    copyModel.TotalFilesSizeToCopy += new FileInfo(file).Length;
                }
                copyModel.FilesSizeLeft = copyModel.TotalFilesSizeToCopy;

                DifferentialCopyListOfFiles(PrioritizedFiles, model, copyModel);
                DifferentialCopyListOfFiles(OtherFiles, model, copyModel);
            }

            model.ProgressStateModel.Name = model.NameSaveWork;
            model.ProgressStateModel.Time = DateTime.Now;
            model.ProgressStateModel.ProgressState = "Inactive";
            model.ProgressStateModel.TotalFilesToCopy = 0;
            model.ProgressStateModel.TotalFilesSizeToCopy = 0;
            model.ProgressStateModel.NbFilesLeft = 0;
            model.ProgressStateModel.FilesSizeLeft = 0;
            model.ProgressStateModel.Percentage = 0;
            model.ProgressStateModel.FilePath = "";
            model.ProgressStateModel.FileDestinationPath = "";

            ProgressStateService.ProgressStateFile();
            return 0;
        }

        private void CompleteCopyListOfFiles(string[] Files, SaveWorkModel model, CopyModel copyModel)
        {
            foreach (string file in Files)
            {
                int timeForCryp = 0;

                string[] ExtensionToCrypt = ConfigurationManager.AppSettings["fileToCryp"].Split(',');

                Stopwatch stopwatch = Stopwatch.StartNew();

                if (ExtensionToCrypt.Contains(Path.GetExtension(file)))
                {
                    timeForCryp = cryptoSoft.cryptoSoftEasySave("-c", $"{file}", $"{file.Replace(copyModel.SourcePath, copyModel.DestinationPath)}");
                }
                else
                {
                    File.Copy(file, file.Replace(copyModel.SourcePath, copyModel.DestinationPath), true);
                }

                stopwatch.Stop();
                double FileTransferTime = stopwatch.Elapsed.TotalSeconds;

                //pourcent = (((copyModel.TotalFilesToCopy - copyModel.NbFilesLeft) / copyModel.TotalFilesToCopy));

                copyModel.Percentage = (((float)copyModel.TotalFilesToCopy - (float)copyModel.NbFilesLeft) / (float)copyModel.TotalFilesToCopy)*100;
                copyModel.NbFilesLeft--;
                copyModel.FilesSizeLeft -= new FileInfo(file).Length;

                LogModel logModel = new LogModel()
                {
                    Name = Path.GetFileName(file),
                    FileSource = file,
                    FileTarget = Path.Combine(copyModel.DestinationPath, Path.GetFileName(file)),
                    DestPath = copyModel.DestinationPath,
                    TimeToCrypt = timeForCryp,
                    FileSize = new FileInfo(file).Length,
                    FileTransferTime = FileTransferTime,
                    Time = DateTime.Now,
                };

                model.ProgressStateModel.Name = model.NameSaveWork;
                model.ProgressStateModel.Time = DateTime.Now;
                model.ProgressStateModel.ProgressState = "Active";
                model.ProgressStateModel.TotalFilesToCopy = copyModel.TotalFilesToCopy;
                model.ProgressStateModel.TotalFilesSizeToCopy = copyModel.TotalFilesSizeToCopy;
                model.ProgressStateModel.NbFilesLeft = copyModel.NbFilesLeft;
                model.ProgressStateModel.FilesSizeLeft = copyModel.FilesSizeLeft;
                model.ProgressStateModel.Percentage= copyModel.Percentage;
                model.ProgressStateModel.FilePath = file;
                model.ProgressStateModel.FileDestinationPath = Path.Combine(copyModel.DestinationPath, Path.GetFileName(file));

                LogService.LogFiles(logModel);
                ProgressStateService.ProgressStateFile();
            }
        }

        private void DifferentialCopyListOfFiles(string[] Files, SaveWorkModel model, CopyModel copyModel)
        {
            foreach (string file in Files)
            {
                if (File.GetLastWriteTime(file) > File.GetLastWriteTime(file.Replace(copyModel.SourcePath, copyModel.DestinationPath)))
                {
                    int timeForCryp = 0;

                    string[] ExtensionToCrypt = ConfigurationManager.AppSettings["fileToCryp"].Split(',');

                    Stopwatch stopwatch = Stopwatch.StartNew();

                    if (ExtensionToCrypt.Contains(Path.GetExtension(file)))
                    {
                        timeForCryp = cryptoSoft.cryptoSoftEasySave("-c", $"{file}", $"{file.Replace(copyModel.SourcePath, copyModel.DestinationPath)}");
                    }
                    else
                    {
                        File.Copy(file, file.Replace(copyModel.SourcePath, copyModel.DestinationPath), true);
                    }

                    stopwatch.Stop();
                    double FileTransferTime = stopwatch.Elapsed.TotalSeconds;
                    int pourcent = (copyModel.NbFilesLeft / copyModel.TotalFilesToCopy) * 100;
                    copyModel.Percentage = (((float)copyModel.TotalFilesToCopy - (float)copyModel.NbFilesLeft) / (float)copyModel.TotalFilesToCopy) * 100;
                    copyModel.NbFilesLeft--;
                    copyModel.FilesSizeLeft -= new FileInfo(file).Length;

                    LogModel logModel = new LogModel()
                    {
                        Name = Path.GetFileName(file),
                        FileSource = file,
                        FileTarget = Path.Combine(copyModel.DestinationPath, Path.GetFileName(file)),
                        DestPath = copyModel.DestinationPath,
                        TimeToCrypt = timeForCryp,
                        FileSize = new FileInfo(file).Length,
                        FileTransferTime = FileTransferTime,
                        Time = DateTime.Now,
                    };

                    model.ProgressStateModel.Name = model.NameSaveWork;
                    model.ProgressStateModel.Time = DateTime.Now;
                    model.ProgressStateModel.ProgressState = "Active";
                    model.ProgressStateModel.TotalFilesToCopy = copyModel.TotalFilesToCopy;
                    model.ProgressStateModel.TotalFilesSizeToCopy = copyModel.TotalFilesSizeToCopy;
                    model.ProgressStateModel.NbFilesLeft = copyModel.NbFilesLeft;
                    model.ProgressStateModel.FilesSizeLeft = copyModel.FilesSizeLeft;
                    model.ProgressStateModel.Percentage= copyModel.Percentage;
                    model.ProgressStateModel.FilePath = file;
                    model.ProgressStateModel.FileDestinationPath = Path.Combine(copyModel.DestinationPath, Path.GetFileName(file));
                    
                    LogService.LogFiles(logModel);
                    ProgressStateService.ProgressStateFile();
                }
            }
        }
    }
}