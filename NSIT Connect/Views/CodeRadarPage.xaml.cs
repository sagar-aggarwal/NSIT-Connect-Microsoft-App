using ExpandableRowListView;
using NSIT_Connect.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class CodeRadarPage : Page, INotifyPropertyChanged
    {
        public CodeRadarPage()
        {
            this.InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private ObservableCollection<ExpandableRowListViewControlItem> _Items = new ObservableCollection<ExpandableRowListViewControlItem>();
        public ObservableCollection<ExpandableRowListViewControlItem> Items
        {
            get { return _Items; }
            set { _Items = value; OnPropertyChanged(nameof(Items)); }
        }

        private async void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var uri = new Uri((e.ClickedItem as CodeRadarItem).Link);
            if (uri != null)
            {
                var success = await Windows.System.Launcher.LaunchUriAsync(uri);
            }
            else
            {
                var mssg = new MessageDialog("NO URL AVAILABLE");
                await mssg.ShowAsync();
            }

        }

        private void GridView_ItemClick_1(object sender, ItemClickEventArgs e)
        {

        }
    }
}
