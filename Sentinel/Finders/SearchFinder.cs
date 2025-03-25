namespace Sentinel.Finders
{
    using Sentinel.Finders.Interfaces;
    using Sentinel.Interfaces;

    public class SearchFinder : Finder, IDefaultInitialisation, ISearchFinder
    {

        public void Initialise()
        {
            Name = "Finder";
            Field = LogEntryFields.Description;
            Mode = MatchMode.CaseInsensitive;
            Pattern = string.Empty;
        }    
    }
}