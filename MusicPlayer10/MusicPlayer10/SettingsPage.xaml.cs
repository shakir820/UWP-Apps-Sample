using MusicPlayer10.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.Storage.Search;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MusicPlayer10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public static SettingsPage current;
        private MainPage rootpage = MainPage.Current;

        public string str;

        public SettingsPage()
        {
            this.InitializeComponent();
            current = this;
        }


        private async void GetSongsFromDirectory()
        {
            var folderPicker = new FolderPicker();
            folderPicker.ViewMode = PickerViewMode.List;
            folderPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add("*");

            StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                // Application now has read/write access to all contents in the picked folder
                // (including other sub-folder contents)
                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                
                //keep track of the specified folder
                if(rootpage.folderInfo.FolderPath != null)
                {
                    rootpage.folderInfo.FolderPath.Add(folder.Path);
                }
                else
                {
                    rootpage.folderInfo.FolderPath = new List<string>();
                    rootpage.folderInfo.FolderPath.Add(folder.Path);
                }
                
                rootpage.savefile();

                //this section will help you to get only music files among various types of files
                List<string> fileTypeFilter = new List<string>();
                fileTypeFilter.Add(".mp3");
                fileTypeFilter.Add(".mp4");
                fileTypeFilter.Add(".wma");

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
                    if (musicProperties.Artist == "")
                    {

                        song.Artist = @"#";
                    }
                    else
                    {
                        song.Artist = musicProperties.Artist;
                    }
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
                    rootpage.Songs.Add(song);
                }
            }
        }

 
        private void pickSongs_Click(object sender, RoutedEventArgs e)
        {
            GetSongsFromDirectory();
        }
    }
}
