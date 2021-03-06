﻿using System;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Animation_Example.AnimationViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddDelete : Page
    {
        public AddDelete()
        {
            this.InitializeComponent();
        }
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (rectangleItems.Items.Count > 0)
                rectangleItems.Items.RemoveAt(0);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Color rectColor = new Color();
            rectColor.R = 200;
            rectColor.A = 250;
            Rectangle myRectangle = new Rectangle();
            myRectangle.CanDrag = true;
            myRectangle.Fill = new SolidColorBrush(rectColor);
            myRectangle.Width = 100;
            myRectangle.Height = 100;
            myRectangle.Margin = new Thickness(10);
            rectangleItems.Items.Add(myRectangle);
        }
    }
}
