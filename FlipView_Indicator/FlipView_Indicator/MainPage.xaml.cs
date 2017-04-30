using System;
using System.Collections.Generic;
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

namespace FlipView_Indicator
{

    public class MyItem
    {
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<MyItem> listImages = new List<MyItem>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            IniImage();
            MyFlipView.ItemsSource = listImages;
        }

        private void IniImage()
        {
            listImages.Add(new MyItem() { ImageName = "abstract-rainbow-color-wallpaper.jpg", ImagePath = "ms-appx:///Assets/abstract-rainbow-color-wallpaper.jpg" });
            listImages.Add(new MyItem() { ImageName = "beautiful-sailboat-hd-wallpapers.jpg", ImagePath = "ms-appx:///Assets/beautiful-sailboat-hd-wallpapers.jpg" });
            listImages.Add(new MyItem() { ImageName = "bluewin-turm-1920x1080-wallpaper-1824.jpg", ImagePath = "ms-appx:///Assets/bluewin-turm-1920x1080-wallpaper-1824.jpg" });
            listImages.Add(new MyItem() { ImageName = "dgdsss.png", ImagePath = "ms-appx:///Assets/dgdsss.png" });
            listImages.Add(new MyItem() { ImageName = "dkdfhghhdfk.png", ImagePath = "ms-appx:///Assets/dkdfhghhdfk.png" });
            listImages.Add(new MyItem() { ImageName = "im.png", ImagePath = "ms-appx:///Assets/im.png" });
        }
    }
}
