namespace Sentinel.Finders
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Sentinel.Finders.Interfaces;
    using Sentinel.Interfaces;
    using Sentinel.Services;

    using WpfExtras;

    public class FinderService<T> : ViewModelBase, IFinderService<T>, IDefaultInitialisation
        where T : class, IFinder
    {
        private readonly CollectionChangeHelper<T> collectionHelper = new CollectionChangeHelper<T>();

        public ICommand FindPrevious { get; private set; }
        public ICommand FindNext { get; private set; }

        public ObservableCollection<T> SearchFinders { get; set; }

        public event FindEventHandler OnFind;

        public FinderService()
        {
            SearchFinders = new ObservableCollection<T>();

            FindPrevious = new DelegateCommand(FindPreviousIndex);
            FindNext = new DelegateCommand(FindNextIndex);

            // Register self as an observer of the collection.
            collectionHelper.OnPropertyChanged += CustomFilterPropertyChanged;

            collectionHelper.ManagerName = "FinderService";
            collectionHelper.NameLookup += e => e.Name;

            SearchFinders.CollectionChanged += collectionHelper.AttachDetach;

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

        private void FindPreviousIndex(object obj)
        {
            if(Messages.Items.IsEmpty) return;
            var currentIndex = Messages.SelectedIndex;
            currentIndex--;
            if(currentIndex < 0)
            {
                currentIndex = 0;
            }
            Messages.ScrollIntoView(Messages.Items.GetItemAt(currentIndex));
            Messages.SelectedItem = Messages.Items.GetItemAt(currentIndex);
            //OnFind?.Invoke(Messages.Items.GetItemAt(SelectedIndex) as ILogEntry);
        }

        private void FindNextIndex(object obj)
        {           
            if(Messages.Items.IsEmpty) return;
            var currentIndex = Messages.SelectedIndex;
            currentIndex+=5;
            if(currentIndex > Messages.Items.Count-1)
            {
                currentIndex =  Messages.Items.Count-1;
            }
            Messages.ScrollIntoView(Messages.Items.GetItemAt(currentIndex));
            Messages.SelectedItem = Messages.Items.GetItemAt(currentIndex);
            //OnFind?.Invoke(Messages.Items.GetItemAt(SelectedIndex) as ILogEntry);
        }

        public bool IsFiltered(ILogEntry entry)
        {
            return true;
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