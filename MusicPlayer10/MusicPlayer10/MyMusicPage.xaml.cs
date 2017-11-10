using MusicPlayer10.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.Storage.Search;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MusicPlayer10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyMusicPage : Page
    {
        public IEnumerable<GroupData> ArtistData;
        public ObservableCollection<Song> Songs;
        private MainPage rootpage;

        public MyMusicPage()
        {
            rootpage = MainPage.Current;
            this.InitializeComponent();
            //InitializeDropShadow(ShadowHost, CircleImage);
            ArtistData  = GroupDataSource.getGroupDataSource();
        }


   

  




        //private void InitializeDropShadow(UIElement shadowHost, Shape shadowTarget)
        //{
        //    Visual hostVisual = ElementCompositionPreview.GetElementVisual(shadowHost);
        //    Compositor compositor = hostVisual.Compositor;

        //    // Create a drop shadow
        //    var dropShadow = compositor.CreateDropShadow();
        //    dropShadow.Color = Windows.UI.Color.FromArgb(255, 75, 75, 80);
        //    dropShadow.BlurRadius = 15.0f;
        //    dropShadow.Offset = new Vector3(2.5f, 2.5f, 0.0f);
        //    // Associate the shape of the shadow with the shape of the target element
        //    dropShadow.Mask = shadowTarget.GetAlphaMask();

        //    // Create a Visual to hold the shadow
        //    var shadowVisual = compositor.CreateSpriteVisual();
        //    shadowVisual.Shadow = dropShadow;

        //    // Add the shadow as a child of the host in the visual tree
        //    ElementCompositionPreview.SetElementChildVisual(shadowHost, shadowVisual);

        //    // Make sure size of shadow host and shadow visual always stay in sync
        //    var bindSizeAnimation = compositor.CreateExpressionAnimation("hostVisual.Size");
        //    bindSizeAnimation.SetReferenceParameter("hostVisual", hostVisual);

        //    shadowVisual.StartAnimation("Size", bindSizeAnimation);
        //}

        //private async void fetchsongsAsync()
        //{
        //    await rootpage.OpenFileforRead();

        //    if(rootpage.folderInfo.FolderPath != null)
        //    {
        //        var addresses = rootpage.folderInfo;

        //        foreach (var add in addresses.FolderPath)
        //        {
        //            List<string> fileTypeFilter = new List<string>();
        //            fileTypeFilter.Add(".mp3");
        //            fileTypeFilter.Add(".mp4");
        //            fileTypeFilter.Add(".wma");

        //            var folder = await StorageFolder.GetFolderFromPathAsync(add);

        //            QueryOptions queryOption = new QueryOptions(CommonFileQuery.OrderByName, fileTypeFilter);
        //            queryOption.FolderDepth = FolderDepth.Deep; //This is for a deep search like search in subfolders
        //            var query = folder.CreateFileQueryWithOptions(queryOption);
        //            var files = await query.GetFilesAsync();

        //            foreach (var file in files)
        //            {
        //                // Store file for future access
        //                var properties = file.Properties;
        //                MusicProperties musicProperties = await properties.GetMusicPropertiesAsync();

        //                var song = new Song();
        //                song.SongFile = file;
        //                song.Name = file.Name;
        //                song.Title = musicProperties.Title;
        //                song.Album = musicProperties.Album;
        //                song.AlbumArtist = musicProperties.AlbumArtist;
        //                song.Artist = musicProperties.Artist;
        //                song.BitRate = musicProperties.Bitrate;
        //                song.Composers = musicProperties.Composers;
        //                song.Conductors = musicProperties.Conductors;
        //                song.Duration = musicProperties.Duration;
        //                song.Genre = musicProperties.Genre;
        //                song.Producers = musicProperties.Producers;
        //                song.Publisher = musicProperties.Publisher;
        //                song.Rating = musicProperties.Rating;
        //                song.TrackNumber = musicProperties.TrackNumber;
        //                song.Writers = musicProperties.Writers;
        //                song.Year = musicProperties.Year;
        //                rootpage.Songs.Add(song);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        return;
        //    }

        //}
        public List<string> fileToken { get; private set; }
    }
}
