using EasySave.lib.Models;
using System.Configuration;
using System.Text.Json;
using System.Xml;

namespace EasySave.lib.Services
{
    public static class Log
    {
        public static int LogFiles(string[] LogArray)
        {
            if (LogArray != null)
            {
                if (ConfigurationManager.AppSettings["typeLogXML"] == "false")
                {
                    List<LogModel> logs = new List<LogModel>();
                    DateTime today = DateTime.Now;
                    string day = today.ToString("dd_MM_yyyy");

                    string DirectoryPath = ConfigurationManager.AppSettings["LogPath"];
                    string LogPath = Path.Combine(DirectoryPath, $"{day}_log.json");

                    LogModel _LogModel = new LogModel
                    {
                        Name = LogArray[0],
                        FileSource = LogArray[1],
                        FileTarget = LogArray[2],
                        destPath = LogArray[3],
                        TimeToCrypt = int.Parse(LogArray[4]),
                        FileSize = int.Parse(LogArray[5]),
                        FileTransferTime = double.Parse(LogArray[6].Replace('.', ',')),
                        time = DateTime.Parse(LogArray[7])
                    };

                    logs.Add(_LogModel);

                    if (!Directory.Exists(DirectoryPath))
                    {
                        Directory.CreateDirectory(DirectoryPath);
                    }

                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        WriteIndented = true
                    };
                    string JsonLog = JsonSerializer.Serialize(logs, options);

                    try
                    {
                        File.AppendAllText(LogPath, JsonLog + Environment.NewLine);
                    }
                    catch
                    {
                        return 1;
                    }

                    return 0;
                }
                else
                {
                    DateTime today = DateTime.Now;
                    string day = today.ToString("dd_MM_yyyy");

                    string DirectoryPath = ConfigurationManager.AppSettings["LogPath"];
                    string LogPath = Path.Combine(DirectoryPath, $"{day}_log.xml");

                    LogModel log = new LogModel
                    {
                        Name = LogArray[0],
                        FileSource = LogArray[1],
                        FileTarget = LogArray[2],
                        destPath = LogArray[3],
                        FileSize = int.Parse(LogArray[4]),
                        FileTransferTime = double.Parse(LogArray[5].Replace('.', ',')),
                        time = DateTime.Parse(LogArray[6])
                    };

                    if (!Directory.Exists(DirectoryPath))
                    {
                        Directory.CreateDirectory(DirectoryPath);
                    }

                    XmlDocument doc = new XmlDocument();

                    if (File.Exists(LogPath))
                    {
                        doc.Load(LogPath);
                    }
                    else
                    {
                        XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                        XmlElement root = doc.DocumentElement;
                        doc.InsertBefore(xmlDeclaration, root);

                        XmlElement logsNode = doc.CreateElement("logs");
                        doc.AppendChild(logsNode);
                    }

                    XmlElement logNode = doc.CreateElement("log");
                    XmlElement nameNode = doc.CreateElement("name");
                    nameNode.InnerText = log.Name;
                    XmlElement fileSourceNode = doc.CreateElement("file_source");
                    fileSourceNode.InnerText = log.FileSource;
                    XmlElement fileTargetNode = doc.CreateElement("file_target");
                    fileTargetNode.InnerText = log.FileTarget;
                    XmlElement destPathNode = doc.CreateElement("destination_path");
                    destPathNode.InnerText = log.destPath;
                    XmlElement fileSizeNode = doc.CreateElement("file_size");
                    fileSizeNode.InnerText = log.FileSize.ToString();
                    XmlElement fileTransferTimeNode = doc.CreateElement("file_transfer_time");
                    fileTransferTimeNode.InnerText = log.FileTransferTime.ToString();
                    XmlElement timeNode = doc.CreateElement("time");
                    timeNode.InnerText = log.time.ToString();

                    logNode.AppendChild(nameNode);
                    logNode.AppendChild(fileSourceNode);
                    logNode.AppendChild(fileTargetNode);
                    logNode.AppendChild(destPathNode);
                    logNode.AppendChild(fileSizeNode);
                    logNode.AppendChild(fileTransferTimeNode);
                    logNode.AppendChild(timeNode);

                    XmlNode logsParentNode = doc.SelectSingleNode("logs");
                    if (logsParentNode != null)
                    {
                        logsParentNode.AppendChild(logNode);
                    }
                    else
                    {
                        XmlElement logsNode = doc.CreateElement("logs");
                        logsNode.AppendChild(logNode);
                        doc.AppendChild(logsNode);
                    }

                    doc.Save(LogPath);

                    return 0;
                }
                
            }
            else
            {
                return 1;
            }
        }
    }
}