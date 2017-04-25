using System;
using System.ComponentModel;
using Windows.UI.Core;
using Windows.UI.ViewManagement;

namespace MultipleViews
{
    public delegate void ViewReleasedHandler(object sender, EventArgs e);

    public sealed class ViewLifeTimeControl : INotifyPropertyChanged
    {
        ///<variable>
        ///<variable>
        CoreDispatcher dispatcher;
        CoreWindow window;
        string title;
        int refCount = 0;
        int viewId;
        bool released = false;

        public event PropertyChangedEventHandler PropertyChanged;

        ///<variables/>

        public static ViewLifeTimeControl CreateForCurrentView()
        {
            return new ViewLifeTimeControl(CoreWindow.GetForCurrentThread());
        }

        private ViewLifeTimeControl(CoreWindow newWindow)
        {
            dispatcher = newWindow.Dispatcher;
            window = newWindow;
            viewId = ApplicationView.GetApplicationViewIdForWindow(window);
            RegisterForEvents();
        }

        private void RegisterForEvents()
        {
            ApplicationView.GetForCurrentView().Consolidated += ViewConsolidated;
        }

        private void ViewConsolidated(ApplicationView sender, ApplicationViewConsolidatedEventArgs args)
        {
            StopViewInUse();
        }

        public void StartViewInUse()
        {
            bool releasedCopy = false;
            int refCountCopy = 0;
            releasedCopy = this.released;
            if (!released)
            {
                refCountCopy = ++refCount;
            }
            if (releasedCopy)
            {
                throw new InvalidOperationException("This view is being disposed");
            }
        }

        public void StopViewInUse()
        {
            int refCountCopy = 0;
            bool releasedCopy = false;
            releasedCopy = this.released;
            if (!released)
            {
                refCountCopy = --refCount;
                if (refCountCopy == 0)
                {
                    var task = dispatcher.RunAsync(CoreDispatcherPriority.Low, FinalizeRelease);
                }
            }
            if (releasedCopy)
            {
                throw new InvalidOperationException("This view is being disposed");
            }
        }

        private void FinalizeRelease()
        {
            bool JustReleased = false;
            if (refCount == 0)
            {
                JustReleased = true;
                released = true;
            }
            if (JustReleased)
            {
                UnregisterForEvents();
                InternalReleased(this, null);
            }
        }

        private void UnregisterForEvents()
        {
            ApplicationView.GetForCurrentView().Consolidated -= ViewConsolidated;
        }

        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Title"));
                }
            }
        }

        public CoreDispatcher Dispatcher
        {
            get { return dispatcher; }
        }
        public int Id
        {
            get { return viewId; }
        }
        public event ViewReleasedHandler Released
        {
            add
            {
                bool releasedCopy = false;
                releasedCopy = released;
                if (!released)
                {
                    InternalReleased += value;
                }
                if (releasedCopy)
                {
                    throw new InvalidOperationException("This view is being disposed");
                }
            }
            remove
            {
                InternalReleased -= value;
            }
        }

        event ViewReleasedHandler InternalReleased;
    }
}