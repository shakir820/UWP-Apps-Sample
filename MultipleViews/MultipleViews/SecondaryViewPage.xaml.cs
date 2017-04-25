using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MultipleViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SecondaryViewPage : Page
    {
        const string EMPTY_TITLE = "<title cleared>";

        ViewLifeTimeControl thisViewControl;
        int mainViewId;
        CoreDispatcher mainDispatcher;

        public SecondaryViewPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            thisViewControl = (ViewLifeTimeControl)e.Parameter;
            mainViewId = ((App)App.Current).MainViewId;
            mainDispatcher = ((App)App.Current).MainDispatcher;

            // When this view is finally release, clean up state
            thisViewControl.Released += ViewLifetimeControl_Released;
        }

        private async void GoToMain_Click(object sender, RoutedEventArgs e)
        {
            await ApplicationViewSwitcher.SwitchAsync(mainViewId);
        }

        private async void HideView_Click(object sender, RoutedEventArgs e)
        {
            await ApplicationViewSwitcher.SwitchAsync(mainViewId,
                ApplicationView.GetForCurrentView().Id,
                ApplicationViewSwitchingOptions.ConsolidateViews);
        }

        private void SetTitle_Click(object sender, RoutedEventArgs e)
        {
            SetTitle(TitleBox.Text);
        }

        private void ClearTitle_Click(object sender, RoutedEventArgs e)
        {
            SetTitle("");
            TitleBox.Text = "";
        }

        private async void SetTitle(string newTitle)
        {
            var thisViewId = ApplicationView.GetForCurrentView().Id;
            ApplicationView.GetForCurrentView().Title = newTitle;

            await mainDispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (newTitle == "") newTitle = EMPTY_TITLE;
                ((App)App.Current).UpdateTitle(newTitle, thisViewId);
            });

        }

        private async void ViewLifetimeControl_Released(Object sender, EventArgs e)
        {
            ((ViewLifeTimeControl)sender).Released -= ViewLifetimeControl_Released;
            // The ViewLifetimeControl object is bound to UI elements on the main thread
            // So, the object must be removed from that thread
            await mainDispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                ((App)App.Current).SecondaryViews.Remove(thisViewControl);
            });

            Window.Current.Close();
        }
    }
}
