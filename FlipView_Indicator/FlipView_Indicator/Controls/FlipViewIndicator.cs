using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel;
using System.IO;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;


namespace FlipView_Indicator.Controls
{
    class FlipViewIndicator: ListBox
    {
        public FlipViewIndicator()
        {
            
        }

        public FlipView flipView
        {
            get { return (FlipView)GetValue(flipViewProperty); }
            set { SetValue(flipViewProperty, value); }
        }

        public DependencyProperty flipViewProperty = DependencyProperty.Register("flipView", typeof(FlipView), 
            typeof(FlipViewIndicator), new PropertyMetadata(null, (depobj, args) =>
        {
            FlipViewIndicator fvi = (FlipViewIndicator)depobj;
            FlipView fv = (FlipView)args.NewValue;
            fvi.ItemsSource = fv.ItemsSource;
            fv.SelectionChanged += (s, e) =>
            {
                fvi.SelectedIndex = fv.SelectedIndex;
            };
            Windows.UI.Xaml.Data.Binding eb = new Windows.UI.Xaml.Data.Binding();
            eb.Mode = BindingMode.TwoWay;
            eb.Source = fv;
            eb.Path = new PropertyPath("SelectedItem");
            fvi.SetBinding(FlipViewIndicator.SelectedItemProperty, eb);
        }));
    }
}
