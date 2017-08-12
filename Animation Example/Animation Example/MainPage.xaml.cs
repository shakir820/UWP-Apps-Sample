using Animation_Example.AnimationViews;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Animation_Example
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Current;

        public MainPage()
        {
            this.InitializeComponent();
            Current = this;
        }

        List<Scenario> scenarios = new List<Scenario>
        {
            new Scenario() { Title = "Add and Delete Animation", ClassType = typeof(AddDelete) },
            new Scenario() { Title = "Drag and Drop Animation", ClassType = typeof(DragAndDrop) },
            new Scenario() { Title = "Fade In and Out Animation", ClassType = typeof(FadeInFadeOut) },
            new Scenario() { Title = "Animation View", ClassType = typeof(AnimationView) },
            new Scenario() { Title = "Content Theme Transition", ClassType = typeof(ContentThemeTransation) },
            new Scenario() {Title = "XAML Style", ClassType = typeof(XAMLStyle)}
        };

        public List<Scenario> Scenarios
        {
            get { return this.scenarios; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Splitter.IsPaneOpen = !Splitter.IsPaneOpen;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Populate the scenario list from the SampleConfiguration.cs file
            ScenarioControl.ItemsSource = scenarios;
            if (Window.Current.Bounds.Width < 640)
            {
                ScenarioControl.SelectedIndex = -1;
            }
            else
            {
                ScenarioControl.SelectedIndex = 0;
            }
        }

        private void ScenarioControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox ScenarioListBox = sender as ListBox;
            Scenario s = ScenarioListBox.SelectedItem as Scenario;
            if (s != null)
            {
                ScenarioFrame.Navigate(s.ClassType);
                if (Window.Current.Bounds.Width < 640)
                    Splitter.IsPaneOpen = false;
            }
        }
    }



    public class Scenario
    {
        public string Title { get; set; }

        public Type ClassType { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
