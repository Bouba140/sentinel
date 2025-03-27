namespace Sentinel.Logs.Interfaces
{
    using System.Collections.Generic;
    using Sentinel.Interfaces;
    using Sentinel.Views.Interfaces;

    public interface ILogFileExporter
    {
        void SaveLogViewerToFile(IWindowFrame windowFrame, string filePath);
        IEnumerable<ILogEntry> GetLogFromFile(string filePath);
    }
}
