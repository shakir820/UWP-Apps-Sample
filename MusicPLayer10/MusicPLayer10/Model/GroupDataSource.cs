using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer10.Model
{
    public class GroupDataSource
    {
        public static IEnumerable<GroupData> getGroupDataSource()
        {
            var Songs = MainPage.Current.Songs;
            var artistData = Songs.GroupBy(ar => ar.Artist, (key, song) => new ArtistData(){ ArtistName = key, Songs = song.ToList() });
            var gpDataSource = artistData.GroupBy(a => a.ArtistName[0], (a, b) => new GroupData() { HeaderTitle = a, Artist = b.ToList() });
            return gpDataSource;
        }
    }
}
