using MusicPlayer10.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Search;
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
        public static MainPage Current;
        public ObservableCollection<Song> Songs;
        public info folderInfo;


        public MainPage()
        {
            this.InitializeComponent();
            Songs = new ObservableCollection<Song>();
            Current = this;
            fetchsongsAsync();
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(SettingsPage));
                NavView.Header = "Settings";
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
                NavView.Header = "Settings";
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


        public async Task OpenFileforRead()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            if (File.Exists(storageFolder.Path + @"\folders.binary"))
            {
                var file = await storageFolder.GetFileAsync("folders.binary");
                BinaryFormatter bf = new BinaryFormatter();
                try
                {
                    FileStream fsin = new FileStream(file.Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using (fsin)
                    {
                        folderInfo = (info)bf.Deserialize(fsin);
                    }
                }
                catch (Exception)
                {
                    return;
                }
            }
            else
            {
                folderInfo = new info();
                return;
            }
        }

        public async void savefile()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.CreateFileAsync("folders.binary", CreationCollisionOption.ReplaceExisting);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fsout = new FileStream(sampleFile.Path, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
            using (fsout)
            {
                bf.Serialize(fsout, folderInfo);
            }
            
        }

        public async void fetchsongsAsync()
        {
            await OpenFileforRead();

            if (folderInfo.FolderPath != null)
            {
                var addresses = folderInfo;

                foreach (var add in addresses.FolderPath)
                {
                    List<string> fileTypeFilter = new List<string>();
                    fileTypeFilter.Add(".mp3");
                    fileTypeFilter.Add(".mp4");
                    fileTypeFilter.Add(".wma");

                    try
                    {
                        var folder = await StorageFolder.GetFolderFromPathAsync(add);

                        QueryOptions queryOption = new QueryOptions(CommonFileQuery.OrderByName, fileTypeFilter);
                        queryOption.FolderDepth = FolderDepth.Deep; //This is for a deep search like search in subfolders
                        var query = folder.CreateFileQueryWithOptions(queryOption);
                        var files = await query.GetFilesAsync();

                        foreach (var file in files)
                        {
                            // Store file for future access
                            var properties = file.Properties;
                            MusicProperties musicProperties = await properties.GetMusicPropertiesAsync();

                            var song = new Song();
                            song.SongFile = file;
                            song.Name = file.Name;
                            song.Title = musicProperties.Title;
                            song.Album = musicProperties.Album;
                            song.AlbumArtist = musicProperties.AlbumArtist;
                            song.Artist = musicProperties.Artist;
                            song.BitRate = musicProperties.Bitrate;
                            song.Composers = musicProperties.Composers;
                            song.Conductors = musicProperties.Conductors;
                            song.Duration = musicProperties.Duration;
                            song.Genre = musicProperties.Genre;
                            song.Producers = musicProperties.Producers;
                            song.Publisher = musicProperties.Publisher;
                            song.Rating = musicProperties.Rating;
                            song.TrackNumber = musicProperties.TrackNumber;
                            song.Writers = musicProperties.Writers;
                            song.Year = musicProperties.Year;
                            Songs.Add(song);
                        }
                    }
                    catch (System.UnauthorizedAccessException)
                    {

                    }
                    
                }
            }
            else
            {
                return;
            }

        }

        public List<string> getArtists()
        {
            List<string> artists = new List<string>();
            List<string> artistsList = new List<string>();
            foreach(var song in Songs)
            {
                artists.Add(song.Artist);
            }

            artistsList = (List<string>)artists.Distinct();

            return artistsList;
        }
    }
}
