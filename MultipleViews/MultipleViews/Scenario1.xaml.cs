using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
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
    public sealed partial class Scenario1 : Page
    {
        const string DEFAULT_TITLE = "New Window";
        MainPage rootPage = MainPage.Current;


        public Scenario1()
        {
            this.InitializeComponent();
            var anchorSizeChoices = GenerateSizePreferenceBinding();
            anchorSizeChoices.Add(new SizePreferenceString() { Preference = ViewSizePreference.UseNone, Title = "UseNone" });
            SizePreferenceChooser.ItemsSource = GenerateSizePreferenceBinding();
            AnchorSizePreferenceChooser.ItemsSource = anchorSizeChoices;
            ViewChooser.ItemsSource = ((App)(App.Current)).SecondaryViews;

            SizePreferenceChooser.SelectedIndex = 0;
            AnchorSizePreferenceChooser.SelectedIndex = 0;
        }

        private List<SizePreferenceString> GenerateSizePreferenceBinding()
        {
            var sizeChoices = new List<SizePreferenceString>
            {
                new SizePreferenceString(){ Preference = ViewSizePreference.Default, Title = "Default"},
                new SizePreferenceString(){ Preference = ViewSizePreference.UseHalf, Title = "UseHalf"},
                new SizePreferenceString(){ Preference = ViewSizePreference.UseLess, Title = "UseLess"},
                new SizePreferenceString(){ Preference = ViewSizePreference.UseMinimum, Title = "UseMinimum"},
                new SizePreferenceString(){ Preference = ViewSizePreference.UseMore, Title = "UseMore"}
            };
            return sizeChoices;
        }


        private async void CreateView_Click(object sender, RoutedEventArgs e)
        {
            ViewLifeTimeControl viewControl = null;
            await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                viewControl = ViewLifeTimeControl.CreateForCurrentView();
                viewControl.Title = DEFAULT_TITLE;
                viewControl.StartViewInUse();
                Frame frame = new Frame();
                frame.Navigate(typeof(SecondaryViewPage), viewControl);
                Window.Current.Content = frame;
                Window.Current.Activate();
                ApplicationView.GetForCurrentView().Title = viewControl.Title;
            });

            ((App)App.Current).SecondaryViews.Add(viewControl);
        }

        private async void ShowAsStandalone_Click(object sender, RoutedEventArgs e)
        {
            var selectedView = ViewChooser.SelectedItem as ViewLifeTimeControl;
            var sizePreference = SizePreferenceChooser.SelectedItem as SizePreferenceString;
            var anchorSizePreference = AnchorSizePreferenceChooser.SelectedItem as SizePreferenceString;

            if (selectedView != null && sizePreference != null && anchorSizePreference != null)
            {
                try
                {
                    selectedView.StartViewInUse();
                    var viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(selectedView.Id,
                        sizePreference.Preference,
                        ApplicationView.GetForCurrentView().Id,
                        anchorSizePreference.Preference);
                    if (!viewShown)
                    {
                        rootPage.NotifyUser("The view was not shown. Make sure it has focus", NotifyType.ErrorMessage);
                    }
                    selectedView.StopViewInUse();
                }
                catch (InvalidOperationException)
                {
                    // The view could be in the process of closing, and
                    // this thread just hasn't updated. As part of being closed,
                    // this thread will be informed to clean up its list of
                    // views (see SecondaryViewPage.xaml.cs)
                }
            }
            else
            {
                rootPage.NotifyUser("Please choose a view to show, a size preference for each view", NotifyType.ErrorMessage);
            }
        }
    }

    public sealed class SizePreferenceString
    {
        public string Title { get; set; }
        public ViewSizePreference Preference { get; set; }
    }
}
