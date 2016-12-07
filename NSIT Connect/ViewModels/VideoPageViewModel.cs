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
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace NSIT_Connect.ViewModels
{
    public class VideoPageViewModel : ViewModelBase
    {
        private string result = null;
        public string nextPageToken = "";
        public string prevPageToken = "";
        public string navigateTo = "next";

        private Visibility _progressVisibility = Visibility.Collapsed;
        public Visibility ProgressVisibility
        { get { return _progressVisibility; } set { Set(() => ProgressVisibility, ref _progressVisibility, value); } }

        private YVideoItem _selected;
        public object Selected
        {
            get { return _selected; }
            set
            {
                var message = value as YVideoItem;
                Set(ref _selected, message);
            }
        }


        private ObservableCollection<YVideoItem> item = new ObservableCollection<YVideoItem>();
        public ObservableCollection<YVideoItem> Item { get { return item; } set { item = value; } }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                
            }
            getinfo();
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
               
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public void GotoVideoDetailPage() =>
                    NavigationService.Navigate(typeof(Views.VideoDetailPage), Selected);

        public async void getinfo()
        {
            Item.Clear();
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                ProgressVisibility = Visibility.Visible;
                string uristring = "https://www.googleapis.com/youtube/v3/playlistItems?part=snippet&playlistId=UUu445B5LTXzkNr5eft8wNHg&key=AIzaSyBgktirlOODUO9zWD-808D7zycmP7smp-Y";
                result = null;
                if (navigateTo == "next")
                {
                    uristring = uristring + "&pageToken=" + nextPageToken;
                }
                else if (navigateTo == "prev")
                {
                    uristring = uristring + "&pageToken=" + prevPageToken;
                }

                var httpClient = new HttpClient();
                var uri = new Uri(uristring);

                try
                {
                    HttpResponseMessage responseMessage = await httpClient.GetAsync(uri);
                    responseMessage.EnsureSuccessStatusCode();
                    result = await responseMessage.Content.ReadAsStringAsync();
                }
                catch (Exception ex) { }


                httpClient.Dispose();
            }
            else
            {
                var mssg = new MessageDialog("No Internet");
                await mssg.ShowAsync();
            }
            if (result != null && result != string.Empty)
            {
                string title = null;
                string description = null;
                string date = null;
                string videoid = null;
                string thumbnail = null;

                JsonObject YTFeed = JsonObject.Parse(result);
                JsonArray YTFeedItems = YTFeed.GetNamedArray("items");

                if (YTFeed.ContainsKey("nextPageToken"))
                {
                    nextPageToken = YTFeed.GetNamedString("nextPageToken");
                    
                }
                else
                {
                    nextPageToken = "";
                }

                if (YTFeed.ContainsKey("prevPageToken"))
                {
                    prevPageToken = YTFeed.GetNamedString("prevPageToken");
                }
                else
                {
                    prevPageToken = "";
                }


                int len = YTFeedItems.Count;
                for (uint i = 0; i < len; i++)
                {

                    if (YTFeedItems.GetObjectAt(i).GetNamedObject("snippet").ContainsKey("title"))
                        title = YTFeedItems.GetObjectAt(i).GetNamedObject("snippet").GetNamedString("title");
                    else
                        title = null;

                    if (YTFeedItems.GetObjectAt(i).GetNamedObject("snippet").ContainsKey("description"))
                        description = YTFeedItems.GetObjectAt(i).GetNamedObject("snippet").GetNamedString("description");
                    else
                        description = null;

                    if (YTFeedItems.GetObjectAt(i).GetNamedObject("snippet").ContainsKey("publishedAt"))
                        date = YTFeedItems.GetObjectAt(i).GetNamedObject("snippet").GetNamedString("publishedAt");
                    else
                        date = null;

                    if (YTFeedItems.GetObjectAt(i).GetNamedObject("snippet").GetNamedObject("resourceId").ContainsKey("videoId"))
                        videoid = YTFeedItems.GetObjectAt(i).GetNamedObject("snippet").GetNamedObject("resourceId").GetNamedString("videoId");
                    else
                        videoid = null;

                    if (YTFeedItems.GetObjectAt(i).GetNamedObject("snippet").GetNamedObject("thumbnails").ContainsKey("maxres"))
                        thumbnail = YTFeedItems.GetObjectAt(i).GetNamedObject("snippet").GetNamedObject("thumbnails").GetNamedObject("maxres").GetNamedString("url");
                    else if(YTFeedItems.GetObjectAt(i).GetNamedObject("snippet").GetNamedObject("thumbnails").ContainsKey("standard"))
                        thumbnail = YTFeedItems.GetObjectAt(i).GetNamedObject("snippet").GetNamedObject("thumbnails").GetNamedObject("standard").GetNamedString("url");
                    else if (YTFeedItems.GetObjectAt(i).GetNamedObject("snippet").GetNamedObject("thumbnails").ContainsKey("high"))
                        thumbnail = YTFeedItems.GetObjectAt(i).GetNamedObject("snippet").GetNamedObject("thumbnails").GetNamedObject("high").GetNamedString("url");
                    else if (YTFeedItems.GetObjectAt(i).GetNamedObject("snippet").GetNamedObject("thumbnails").ContainsKey("medium"))
                        thumbnail = YTFeedItems.GetObjectAt(i).GetNamedObject("snippet").GetNamedObject("thumbnails").GetNamedObject("medium").GetNamedString("url");
                    else
                        thumbnail = YTFeedItems.GetObjectAt(i).GetNamedObject("snippet").GetNamedObject("thumbnails").GetNamedObject("default").GetNamedString("url");
                    DateTime dt = Convert.ToDateTime(date);
                    Item.Add(new YVideoItem() { Date = dt.ToString("d MMM , yyy"), Descrption = description, Thumbnail = new Uri(thumbnail), Title = title, VideoID = videoid });


                }
                

            }
            ProgressVisibility = Visibility.Collapsed;
        }
    }
}
