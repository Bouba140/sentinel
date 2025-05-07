namespace Sentinel.Logs
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Sentinel.Interfaces;
    using Sentinel.Logs.Interfaces;
    using Sentinel.NLog;
    using Sentinel.Views.Interfaces;

    public class LogFileExporter : ILogFileExporter
    {
        public LogFileExporter()
        {
        }

        public void SaveLogViewerToFile(IWindowFrame windowFrame, string filePath)
        {
            // Check if file exists; delete it
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (var fs = File.Create(filePath))
            {
                var messages = windowFrame.PrimaryView.Messages;
                lock (messages)
                {
                    foreach (var msg in messages)
                    {
                        AddText(
                            fs,
                            $"{msg.DateTime.ToString("yyyy-MM-dd HH:mm:ss.ffff")}|{msg.Type}|{msg.System}|{msg.Description}\r\n");
                    }
                }
            }
        }

        public IEnumerable<ILogEntry> GetLogFromFile(string filePath)
        {
            var logEntries = new List<ILogEntry>();

            if (File.Exists(filePath))
            {
                using (var fs = File.OpenRead(filePath))
                {
                    using (var sr = new StreamReader(fs))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 4)
                            {
                                var entry = new LogEntry
                                {
                                    DateTime = DateTime.ParseExact(parts[0],
                                        "yyyy-MM-dd HH:mm:ss.ffff",
                                        System.Globalization.CultureInfo.InvariantCulture),
                                    Type = parts[1],
                                    System = parts[2],
                                    Description = parts[3],
                                    Source = "Import",
                                    MetaData = new Dictionary<string, object>()
                                };
                                logEntries.Add(entry);
                            }
                        }
                    }
                }
            }

            return logEntries;
        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
    }
}