using NSIT_Connect.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;

namespace NSIT_Connect.ViewModels
{
    class HangoutPageViewModel : ViewModelBase
    {
        private const string MAIN_HTTP = "https://maps.googleapis.com/maps/api/place/";
        private const string NEARBYPLACES = "nearbysearch";
        private const string LOCATION = "/json?location=";
        private const string QUERY = "/xml?query=";
        private const string RADIUS = "&radius=";
        private int radius = 1000;
        private const string TYPE = "&types=";
        private const string KEY = "&key=AIzaSyBLSYsHaIe7euGK_glMbU98ZW9SDNBcEkM";

        private LocationItem _selected = default(LocationItem);
        public LocationItem Selected
        {
            get { return _selected; }
            set
            {
                var message = value as LocationItem;
                Set(ref _selected, message);
            }
        }

        private ObservableCollection<HangoutItem> item = new ObservableCollection<HangoutItem>();
        public ObservableCollection<HangoutItem> Item { get { return item; } set { item = value; } }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            Selected = (suspensionState.ContainsKey(nameof(Selected))) ? suspensionState[nameof(Selected)] as LocationItem : parameter as LocationItem;
            Selected.Name = "#" + Selected.Name.ToLower();
            Item.Add(new HangoutItem() { Name = "Rajeev Sagar Aggarawl", Vicinity = "Khanpurasdasdasdasdasdas ", Photo_Ref = new Uri("ms-appx:///Assets/Location/nsit_hotspots.jpg"),Rating = 2.5, OpenNowString = "Closed Now" });
            if (NetworkInterface.GetIsNetworkAvailable())
            {

            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState[nameof(Selected)] = Selected;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }
    }
}
