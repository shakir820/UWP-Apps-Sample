using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Animation_Example
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AnimationView : Page
    {
        public AnimationView()
        {
            this.InitializeComponent();
        }
       
        public ObservableCollection<profile> Profiles = new ObservableCollection<profile>();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            populateProfiles();
            ProfilesGridView.ItemsSource = Profiles;
        }

        public void populateProfiles()
        {
            Profiles.Add(new profile() { ID = 1, description = "Internship at Standard Bank Limited (SBL)", image = "ms-appx:///ProfilePic/Rajib Ahmed", name = "Rajib Ahmed" });
            Profiles.Add(new profile() { ID = 2, description = "QA Engireer at Implevista", image = "ms-appx:///ProfilePic/Rukaiya Binte Asad.jpg", name = "Rukaiya Binte Asad" });
            Profiles.Add(new profile() { ID = 3, description = "Student at North South University", image = "ms-appx:///ProfilePic/Tasfia Binte Mozumder.jpg", name = "Tasfia Binte Mozumder" });
            Profiles.Add(new profile() { ID = 4, description = "Sr. Software Engineer at ImpleVista BD Ltd", image = "ms-appx:///ProfilePic/Md Obaidur Rahman.jpg", name = "Md Obaidur Rahman" });
            Profiles.Add(new profile() { ID = 5, description = "IELTs instructor at St John's tutorial", image = "ms-appx:///ProfilePic/Tahrina Chowdhury.jpg", name = "Tahrina Chowdhury" });
            Profiles.Add(new profile() { ID = 6, description = "Assistant Engineer (Electrical ). at Education engineering department .Bangladesh", image = "ms-appx:///ProfilePic/Jahangir Iqbal.jpg", name = "Jahangir Iqbal" });
            Profiles.Add(new profile() { ID = 7, description = "Ceo & Managing Director at Persona Persona", image = "ms-appx:///ProfilePic/kaniz Almas khan.jpg", name = "kaniz Almas khan" });
            Profiles.Add(new profile() { ID = 8, description = "Research Coordinator and Focal Person (Programmes) at Khan Foundation", image = "ms-appx:///ProfilePic/Naureen Khan.jpg", name = "Naureen Khan" });
            Profiles.Add(new profile() { ID = 9, description = "Lecturer at Northern University Bangladesh", image = "ms-appx:///ProfilePic/Kazi Farhana Yeasmin.jpg", name = "Kazi Farhana Yeasmin" });
            Profiles.Add(new profile() { ID = 10, description = "A developer at Microsoft", image = "ms-appx:///ProfilePic/Jack Ma.jpg", name = "Jack Ma" });
         }

        private void ProfilesGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var container = ProfilesGridView.ContainerFromItem(e.ClickedItem) as GridViewItem;
            if(container != null)
            {
                var root = (FrameworkElement)container.ContentTemplateRoot;
                var image = (UIElement)root.FindName("ProPicture");

                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Image", image);
            }

            var item = (profile)e.ClickedItem;

            Transitions = new TransitionCollection();
            Transitions.Add(new ContentThemeTransition());

            Frame.Navigate(typeof(ProfileView), item);
        }
    }

    public class profile
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string description { get; set; }
    }
}
