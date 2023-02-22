using EasySave.lib.Models;
using System.Configuration;
using System.Diagnostics;

namespace EasySave.lib.Services
{
    public class SaveWorkService
    {
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
                    timeForCryp = cryptoSoft.cryptoSoftEasySave("-c", $"{file}", $"{Path.Combine(copyModel.DestinationPath, Path.GetFileName(file))}");
                }
                else
                {
                    File.Copy(file, file.Replace(copyModel.SourcePath, copyModel.DestinationPath), true);
                }

                stopwatch.Stop();
                double FileTransferTime = stopwatch.Elapsed.TotalSeconds;

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
                        timeForCryp = cryptoSoft.cryptoSoftEasySave("-c", $"{file}", $"{Path.Combine(copyModel.DestinationPath, Path.GetFileName(file))}");
                    }
                    else
                    {
                        File.Copy(file, file.Replace(copyModel.SourcePath, copyModel.DestinationPath), true);
                    }

                    stopwatch.Stop();
                    double FileTransferTime = stopwatch.Elapsed.TotalSeconds;

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
                    model.ProgressStateModel.FilePath = file;
                    model.ProgressStateModel.FileDestinationPath = Path.Combine(copyModel.DestinationPath, Path.GetFileName(file));

                    LogService.LogFiles(logModel);
                    ProgressStateService.ProgressStateFile();
                }
            }
        }
    }
}