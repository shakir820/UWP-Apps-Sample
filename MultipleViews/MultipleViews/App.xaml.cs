using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace MultipleViews
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        private CoreDispatcher mainDispatcher;
        private int mainViewId;
        public ObservableCollection<ViewLifeTimeControl> SecondaryViews = new
            ObservableCollection<ViewLifeTimeControl>();

        public CoreDispatcher MainDispatcher { get { return mainDispatcher; } }
        public int MainViewId { get { return mainViewId; } }
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            InitializeMainPage(e.PreviousExecutionState, "");
            Window.Current.Activate();
        }

        private bool TryFindViewLifetimeControlForViewId(int ViewId, out ViewLifeTimeControl foundData)
        {
            foreach (var viewLifeTimeControl in SecondaryViews)
            {
                if (viewLifeTimeControl.Id == ViewId)
                {
                    foundData = viewLifeTimeControl;
                    return true;
                }
            }
            foundData = null;
            return false;
        }

        private void InitializeMainPage(ApplicationExecutionState previousExecutionState, string arguments)
        {
            Frame rootFrame = CreateRootFrame();
            if (rootFrame.Content == null || !String.IsNullOrEmpty(arguments))
            {
                rootFrame.Navigate(typeof(MainPage), arguments);
            }
        }

        private void InitializeRootFrame()
        {
            mainDispatcher = Window.Current.Dispatcher;
            mainViewId = ApplicationView.GetForCurrentView().Id;
        }

        private Frame CreateRootFrame()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
            {
                rootFrame = new Frame();
                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];
                rootFrame.NavigationFailed += OnNavigationFailed;
                InitializeRootFrame();
                Window.Current.Content = rootFrame;
            }
            return rootFrame;
        }

        public void UpdateTitle(string newTitle, int viewId)
        {
            ViewLifeTimeControl foundData;
            if (TryFindViewLifetimeControlForViewId(viewId, out foundData))
            {
                foundData.Title = newTitle;
            }
            else
            {
                throw new KeyNotFoundException("Couldn't find the view ID in the collection");
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
