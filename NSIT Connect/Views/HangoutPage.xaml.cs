using NSIT_Connect.Models;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace NSIT_Connect.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HangoutPage : Page
    {

        public HangoutPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        private void Main_Grid_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selecteditem = e.ClickedItem as HangoutItem;
            ViewModel.SelectedHangout = selecteditem;
            ViewModel.GotoHangoutDetailPage();
        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            var newvalue = e.NewValue;
            ViewModel.Radius = newvalue*1000;
            ViewModel.RadiusText = newvalue.ToString() + "(km)";
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.gethangoutlist();
        }
    }
}
