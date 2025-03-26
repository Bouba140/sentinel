namespace Sentinel.Finders
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Sentinel.Finders.Interfaces;
    using Sentinel.Interfaces;
    using Sentinel.Services;

    using WpfExtras;

    public class FinderService<T> : ViewModelBase, IFinderService<T>, IDefaultInitialisation
        where T : class, IFinder
    {
        public ObservableCollection<T> SearchFinders { get; set; }

        public FinderService()
        {
            SearchFinders = new ObservableCollection<T>();

            var searchFinder = ServiceLocator.Instance.Get<ISearchFinder>();

            if (searchFinder != null)
            {
                SearchFinders.Add(searchFinder as T);
            }
            else
            {
                Debug.Fail("The search finder is null.");
            }
        }

        public ListView Messages { get; set; }

        public void Initialise()
        {

        }

        public bool IsMatch(ILogEntry entry)
        {
            var searchFinder = SearchFinders.FirstOrDefault();
            if (searchFinder == null)
            {
                return false;
            }
            return searchFinder.IsMatch(entry);
        }

        private void CustomFilterPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var finder = sender as Finder;
            if (finder != null)
            {
                Trace.WriteLine(
                    $"FindergService saw some activity on {finder.Name} (IsEnabled = {finder.Enabled})");
            }

            OnPropertyChanged(string.Empty);
        }
    }
}