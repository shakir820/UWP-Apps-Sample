using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MusicPlayer10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(SettingsPage));
            }
            else
            {
                switch (args.InvokedItem)
                {
                    case "MyMusic":
                        ContentFrame.Navigate(typeof(MyMusicPage));
                        NavView.Header = "My Music";
                        break;

                    case "RecentPlays":
                        ContentFrame.Navigate(typeof(RecentPlaysPage));
                        NavView.Header = "Recent Plays";
                        break;

                    case "NowPlaying":
                        ContentFrame.Navigate(typeof(NowPlayingPage));
                        NavView.Header = "Now Playing";
                        break;

                    case "Playlist":
                        ContentFrame.Navigate(typeof(PlaylistPage));
                        NavView.Header = "Playlist";
                        break;
                }
            }

        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                ContentFrame.Navigate(typeof(SettingsPage));
            }
            else
            {

                NavigationViewItem item = args.SelectedItem as NavigationViewItem;

                switch (item.Tag)
                {
                    case "MyMusic":
                        ContentFrame.Navigate(typeof(MyMusicPage));
                        NavView.Header = "My Music";
                        break;

                    case "RecentPlays":
                        ContentFrame.Navigate(typeof(RecentPlaysPage));
                        NavView.Header = "Recent Plays";
                        break;

                    case "NowPlaying":
                        ContentFrame.Navigate(typeof(NowPlayingPage));
                        NavView.Header = "Now Playing";
                        break;

                    case "Playlist":
                        ContentFrame.Navigate(typeof(PlaylistPage));
                        NavView.Header = "Playlist";
                        break;
                }
            }

        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }


}
