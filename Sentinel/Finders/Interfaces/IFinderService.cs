namespace Sentinel.Finders.Interfaces
{
    using Sentinel.Interfaces;

    public interface IFinderService<T>
    {  
        bool IsMatch(ILogEntry entry);
    }
}