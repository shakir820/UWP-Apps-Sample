using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace MusicPlayer10.Model
{
    public class Song
    {
        public int ID { get; set; }
        public string AlbumArtist { get; set; }
        public IList<string> Genre { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public StorageFile SongFile { get; set; }
        public BitmapImage AlbumCover { get; set; }
        public string Subtitle { get; set; }
        public uint Rating { get; set; }
        public string Contributing_Artist { get; set; }
        public uint Year { get; set; }
        public TimeSpan Duration { get; set; }
        public uint BitRate { get; set; }
        public string Publisher { get; set; }
        public IList<string> Composers { get; set; }
        public IList<string> Conductors { get; set; }
        public uint TrackNumber { get; set; }
        public IList<string> Producers { get; set; }
        public string Name { get; set; }
        public string ItemType {get; set; }
        public IList<string> Writers { get; set; }
        public string FolderPath { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public double Size { get; set; }
    }


}
