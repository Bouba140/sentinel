namespace Sentinel.Finders.Interfaces
{
    using System.Runtime.Serialization;

    using Sentinel.Interfaces;

    public interface IFinder
    {        
        [DataMember]
        string Name { get; set; }
        [DataMember]
        bool Enabled { get; set; }
        [DataMember]
        string Pattern { get; set; }
        [DataMember]
        string Description { get; }
        [DataMember]
        LogEntryFields Field { get; set; }
        [DataMember]
        MatchMode Mode { get; set; }

        bool IsMatch(ILogEntry entry);
    }
}