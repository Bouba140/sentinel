namespace Sentinel.Finders
{
    using System.Runtime.Serialization;

    using Sentinel.Finders.Interfaces;
    using Sentinel.Interfaces;

    [DataContract]
    public class SearchFinder : Finder, IDefaultInitialisation, ISearchFinder
    {
        public void Initialise()
        {
            Name = "Finder";
            Field = LogEntryFields.Description;
            Pattern = string.Empty;
        }
    }
}