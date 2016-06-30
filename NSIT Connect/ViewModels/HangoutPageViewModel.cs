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
using Windows.UI.Xaml;
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
        private const string KEY = "&key=AIzaSyBAuY7uwzJkS1d1Cp8WLYphhs4UuAZ7ZL4";
        private string URL = null;
        private string result = null;

        private Visibility _progressVisibility = Visibility.Collapsed;
        public Visibility ProgressVisibility
        { get { return _progressVisibility; }  set { Set(() => ProgressVisibility, ref _progressVisibility, value); } }


        string[] defaulthangout = {
              "ms-appx:///Assets/Location/nsit_hotspots.jpg",
              "ms-appx:///Assets/Location-Default/cafe_hangout.jpg",
              "ms-appx:///Assets/Location-Default/nightclub_hangout.jpg",
              "ms-appx:///Assets/Location-Default/rest_hangout2.jpg",
              "ms-appx:///Assets/Location-Default/shoppingmall_hangout.jpg",
              "ms-appx:///Assets/Location-Default/bowling_hangout.jpg",
              "ms-appx:///Assets/Location-Default/food_hangout.jpg",
              "ms-appx:///Assets/Location-Default/movie_hangout.jpg",
              "ms-appx:///Assets/Location-Default/amuse_hangout.jpg",
              "ms-appx:///Assets/Location-Default/park_hangout.jpg",

        };


        private LocationItem _selected;
        public LocationItem Selected
        {
            get { return _selected; }
            set
            {
                var message = value as LocationItem;
                Set(ref _selected, message);
            }
        }

        private HangoutItem _selectedhangout = default(HangoutItem);
        public object SelectedHangout
        {
            get { return _selectedhangout; }
            set
            {
                var message = value as HangoutItem;
                Set(ref _selectedhangout, message);
            }
        }


        private ObservableCollection<HangoutItem> item = new ObservableCollection<HangoutItem>();
        public ObservableCollection<HangoutItem> Item { get { return item; } set { item = value; } }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                SelectedHangout = suspensionState[nameof(SelectedHangout)];
            }
            ProgressVisibility = Visibility.Visible;
            Selected = (suspensionState.ContainsKey(nameof(Selected))) ? suspensionState[nameof(Selected)] as LocationItem : parameter as LocationItem;
            Selected.Name = "#" + Selected.Name.ToLower();
            Item.Clear();
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
                suspensionState[nameof(SelectedHangout)] = SelectedHangout;
                suspensionState[nameof(Selected)] = Selected;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public void GotoHangoutDetailPage() =>
                    NavigationService.Navigate(typeof(Views.HangoutDetailPage), SelectedHangout);

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
                    string vicinity = null;
                    string opennow = "No Information";
                    double longitude = -1;
                    double lattitude = -1;
                    double photowidth = 0;
                    double photoheight = 0;
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
                        rating = jplacearray.GetObjectAt(i).GetNamedNumber("rating");
                    else
                        rating = 0;

                    if (jplacearray.GetObjectAt(i).ContainsKey("vicinity"))
                        vicinity = jplacearray.GetObjectAt(i).GetNamedString("vicinity");
                    else
                        vicinity = null;

                    if (jplacearray.GetObjectAt(i).GetNamedObject("geometry").GetNamedObject("location").ContainsKey("lng"))
                    {
                        JsonObject jobject = jplacearray.GetObjectAt(i).GetNamedObject("geometry").GetNamedObject("location").GetObject();
                        longitude = -1;
                        longitude = jobject.GetNamedNumber("lng");
                        lattitude = jobject.GetNamedNumber("lat");
                    }
                    if (jplacearray.GetObjectAt(i).ContainsKey("photos"))
                    {
                        JsonObject obj = jplacearray.GetObjectAt(i).GetNamedArray("photos").GetObjectAt(0);
                        phtotref = obj.GetNamedString("photo_reference");
                        photoheight = obj.GetNamedNumber("height");
                        photowidth = obj.GetNamedNumber("width");
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
                    string temp = null;
                    if (phtotref != null)
                        temp = "https://maps.googleapis.com/maps/api/place/photo?maxheight=" + photoheight + "&maxwidth=" + photowidth +
                        "&photoreference=" + phtotref + KEY;
                    else
                        temp = defaulthangout[Selected.Number];

                    Item.Add(new HangoutItem()
                    {
                        Name = name,
                        Icon = icon,
                        Place_ID = place_id,
                        Photo_Ref = new Uri(temp),
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
            ProgressVisibility = Visibility.Collapsed;
        }
    }
}
