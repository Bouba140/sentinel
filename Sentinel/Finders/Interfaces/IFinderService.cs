namespace Sentinel.Finders.Interfaces
{
    using System.Collections.ObjectModel;
    using System.Windows.Controls;
    using Sentinel.Interfaces;

    public delegate void FindEventHandler(ILogEntry selectedMessage);
    public interface IFinderService<T>
    {        
        event FindEventHandler OnFind;
        ListView Messages { get; set; }
    }
}