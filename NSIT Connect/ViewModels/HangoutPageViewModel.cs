using NSIT_Connect.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.Data.Json;
using Windows.Devices.Geolocation;
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
        private int radius = 5000;
        private const string TYPE = "&types=";
        private const string KEY = "&key=AIzaSyBLSYsHaIe7euGK_glMbU98ZW9SDNBcEkM";
        private string URL = null;
        private string result = null;

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
            Item.Add(new HangoutItem() { Name = "Rajeev Sagar Aggarawl", Vicinity = "Khanpurasdasdasdasdasdas ", Photo_Ref = new Uri("ms-appx:///Assets/Location/nsit_hotspots.jpg"), Rating = 2.5, OpenNowString = "Closed Now" });

            if (NetworkInterface.GetIsNetworkAvailable())
            {
                gethangoutlist();
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

        public async void  gethangoutlist()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            if(accessStatus == GeolocationAccessStatus.Allowed)
            {
                Geolocator geolocator = new Geolocator();
                Geoposition pos = await geolocator.GetGeopositionAsync();
                URL = MAIN_HTTP + NEARBYPLACES + LOCATION + pos.Coordinate.Latitude.ToString() + "," + pos.Coordinate.Longitude.ToString()
                +RADIUS + radius.ToString() + TYPE + Selected.Key + KEY;
            }
            if(URL != null){
                var httpClient = new HttpClient();
                var uri = new Uri(URL);

                try
                {
                    HttpResponseMessage responseMessage = await httpClient.GetAsync(uri);
                    responseMessage.EnsureSuccessStatusCode();
                    result = await responseMessage.Content.ReadAsStringAsync();
                }
                catch (Exception ex) { }
                httpClient.Dispose();
            }
            if (result != null)
            {
                JsonObject jplaceobject = JsonObject.Parse(result);
                JsonArray jplacearray = jplaceobject.GetNamedArray("results");

                //if (jplaceobject.GetNamedString("status").Equals("ZERO_RESULTS"))
                //    Toast.makeText(getApplicationContext(), "No " + HangoutPlaces[choice] + " Present in Current Radius", Toast.LENGTH_SHORT).show();

                for (uint i = 0; i < jplacearray.Count; i++)
                {
                    string name = null;
                    string icon = null;
                    string place_id = null;
                    string phtotref = null;
                    string longitude = null;
                    string lattitude = null;
                    string vicinity = null;
                    string opennow = null;
                    float photowidth = 0;
                    float photoheight = 0;
                    double rating = -1;

                    if (jplacearray.GetObjectAt(i).ContainsKey("icon"))
                        icon = jplacearray.GetObjectAt(i).GetNamedString("icon");
                    else
                        icon = null;

                    if (jplacearray.GetObjectAt(i).ContainsKey("place_id"))
                        place_id = jplacearray.GetObjectAt(i).GetNamedString("place_id");
                    else
                        place_id = null;

                    if (jplacearray.GetObjectAt(i).ContainsKey("name"))
                        name = jplacearray.GetObjectAt(i).GetNamedString("name");
                    else
                        name = null;

                    if (jplacearray.GetObjectAt(i).ContainsKey("rating"))
                        rating = Convert.ToDouble(jplacearray.GetObjectAt(i).GetNamedString("rating"));
                    else
                        rating = 0;

                    if (jplacearray.GetObjectAt(i).ContainsKey("vicinity"))
                        vicinity = jplacearray.GetObjectAt(i).GetNamedString("vicinity");
                    else
                        vicinity = null;

                    if (jplacearray.GetObjectAt(i).GetNamedObject("geometry").GetNamedObject("location").ContainsKey("lng"))
                    {
                        JsonObject jobject = jplacearray.GetObjectAt(i).GetNamedObject("geometry").GetNamedObject("location").GetObject();
                        longitude = "ssss";
                        //longi = jobject.GetNamedString("lng");
                        //latti = jobject.GetNamedString("lat");
                    }
                    if (jplacearray.GetObjectAt(i).ContainsKey("photos"))
                    {
                        JsonObject obj = jplacearray.GetObjectAt(i).GetNamedArray("photos").GetObjectAt(0);
                        phtotref = obj.GetNamedString("photo_reference");
                        photoheight = (float)Convert.ToDouble(obj.GetNamedString("height"));
                        photowidth = (float)Convert.ToDouble(obj.GetNamedString("width"));
                    }
                    else
                    {
                        phtotref = null;
                        photoheight = 0;
                        photowidth = 0;
                    }

                    if (jplacearray.GetObjectAt(i).ContainsKey("opening_hours"))
                    {
                        bool open = jplacearray.GetObjectAt(i).GetNamedObject("opening_hours").GetNamedBoolean("open_now");
                        if (open)
                            opennow = "Open Now";
                        else
                            opennow = "Closed Now";
                    }
                    Item.Add(new HangoutItem()
                    {
                        Name = name,
                        Icon = icon,
                        Place_ID = place_id,
                        Photo_Ref = new Uri(icon),
                        Longi = longitude,
                        Latii = lattitude,
                        Vicinity = vicinity,
                        OpenNowString = opennow,
                        PhotoWidth = photowidth,
                        PhotoHeight = photoheight,
                        Rating = rating
                    });
                }
            }
        }
    }
}
