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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Animation_Example
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProfileView : Page
    {
        public ProfileView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var pro = (profile)e.Parameter;
            var imageUrl = (string)pro.image;

            profilePicture.Source = new BitmapImage(new Uri((string)imageUrl));

            var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("Image");
            if (animation != null)
            {
                profilePicture.Opacity = 0;
                // Wait for image opened. In future Insider Preview releases, this won't be necessary.
                profilePicture.ImageOpened += (sender_, e_) =>
                {
                    profilePicture.Opacity = 1;
                    animation.TryStart(profilePicture);
                };
            }
        }
    }
}
