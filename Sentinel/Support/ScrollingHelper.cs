namespace Sentinel.Support
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Threading;
    using Sentinel.Interfaces;

    public static class ScrollingHelper
    {
        public delegate void VoidFunctionHandler(ListBox listBox);
        public delegate void VoidFunctionItemHandler(ListBox listBox, ILogEntry item);

        public static Visual GetDescendantByType(Visual element, Type type)
        {
            if (element != null)
            {
                if (element.GetType() != type)
                {
                    Visual foundElement = null;
                    (element as FrameworkElement)?.ApplyTemplate();

                    for (var i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
                    {
                        var visual = VisualTreeHelper.GetChild(element, i) as Visual;
                        foundElement = GetDescendantByType(visual, type);
                        if (foundElement != null)
                        {
                            break;
                        }
                    }

                    return foundElement;
                }

                return element;
            }

            return null;
        }

        public static void ScrollToEnd(Dispatcher dispatcher, ListBox listBox)
        {
            if (dispatcher.CheckAccess())
            {
                SelectLastEntry(listBox);
            }
            else
            {
                dispatcher.BeginInvoke(
                    DispatcherPriority.Send,
                    new VoidFunctionHandler(SelectLastEntry),
                    listBox);
            }
        }

        public static void ScrollToItem(Dispatcher dispatcher, ListBox listBox, ILogEntry item)
        {
            if (dispatcher.CheckAccess())
            {
                listBox.ScrollIntoView(item);
            }
            else
            {
                dispatcher.BeginInvoke(
                    DispatcherPriority.Send,
                    new VoidFunctionItemHandler(SelectEntry),
                    listBox,
                    item);
            }
        }

        private static void SelectLastEntry(ListBox listBox)
        {
            var scrollViewer = GetDescendantByType(listBox, typeof(ScrollViewer)) as ScrollViewer;
            scrollViewer?.ScrollToEnd();
        }

        private static void SelectEntry(ListBox listBox, ILogEntry item)
        {
            listBox.ScrollIntoView(item);
        }
    }
}