using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Shadow_Background
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Visual _shadowContainer;
        private Compositor _compositor;
        private SpriteVisual _imageVisual;
        private CompositionImage _image;
        private ManagedSurface _imageMaskSurface;
        private CompositionMaskBrush _maskBrush;
        private bool _isMaskEnabled;


        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Get backing visual from shadow container and interop compositor
            _shadowContainer = ElementCompositionPreview.GetElementVisual(ShadowContainer);
            _compositor = _shadowContainer.Compositor;

            // Get CompositionImage, its sprite visual
            _image = VisualTreeHelperExtensions.GetFirstDescendantOfType<CompositionImage>(ShadowContainer);
            _imageVisual = _image.SpriteVisual;

            // Load mask asset onto surface using helpers in SamplesCommon
            _imageMaskSurface = ImageLoader.Instance.LoadCircle(200, Colors.White);

            // Get surface brush from composition image
            CompositionSurfaceBrush source = _image.SurfaceBrush as CompositionSurfaceBrush;

            // Create mask brush for toggle mask functionality
            _maskBrush = _compositor.CreateMaskBrush();
            _maskBrush.Mask = _imageMaskSurface.Brush;
            _maskBrush.Source = source;

            // Initialize toggle mask
            _isMaskEnabled = false;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_imageMaskSurface != null)
            {
                _imageMaskSurface.Dispose();
            }
        }
    }
}
