namespace Sentinel.Finders.Interfaces
{
    using System.Runtime.Serialization;

    using Sentinel.Interfaces;

    public interface IFinder
    {
        string Name { get; set; }
        bool Enabled { get; set; }
        string Pattern { get; set; }
        string Description { get; }
        LogEntryFields Field { get; set; }
        MatchMode Mode { get; set; }

        bool IsMatch(ILogEntry entry);
    }
}