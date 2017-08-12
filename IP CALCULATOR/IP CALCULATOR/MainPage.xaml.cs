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

namespace IP_CALCULATOR
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < 256; i++)
            {
                ComboBoxItem a = new ComboBoxItem();
                ComboBoxItem b = new ComboBoxItem();
                ComboBoxItem c = new ComboBoxItem();
                ComboBoxItem d = new ComboBoxItem();

                a.Content = i.ToString();
                b.Content = i.ToString();
                c.Content = i.ToString();
                d.Content = i.ToString();

                IP1stOctet.Items.Add(a);
                IP2ndOctet.Items.Add(b);
                IP3rdOctet.Items.Add(c);
                IP4thOctet.Items.Add(d);
            }

            for(int i = 1; i <= 32; i++)
            {
                ComboBoxItem a = new ComboBoxItem();
                a.Content = i.ToString();
                CIDRcomboBox.Items.Add(a);
            }
        }

        private void cal_Subnet_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem a =  (ComboBoxItem)IP1stOctet.SelectedItem;
            byte ip_1st_octet = Byte.Parse((string)a.Content);            
            a = (ComboBoxItem)IP2ndOctet.SelectedItem;
            byte ip_2nd_octet = Byte.Parse((string)a.Content);
            a = (ComboBoxItem)IP3rdOctet.SelectedItem;
            byte ip_3rd_octet = Byte.Parse((string)a.Content);
            a = (ComboBoxItem)IP4thOctet.SelectedItem;
            byte ip_4th_octet = Byte.Parse((string)a.Content);
            int cidr = int.Parse((string)((ComboBoxItem)CIDRcomboBox.SelectedItem).Content);

            IP_Address ip = new IP_Address(ip_1st_octet, ip_2nd_octet, ip_3rd_octet, ip_4th_octet);

            IP_Address subnet = IP_config.get_SubnetNetwork(ip, cidr);

            string sub = String.Format("{0}.{1}.{2}.{3}", subnet.first_Octet, 
                subnet.second_Octet, subnet.third_Octet, subnet.fourth_Octet);

            SubnetTextBlock.Text = sub;
        }
    }
}