
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
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace NSIT_Connect.ViewModels
{
    class CodeRadarPageViewModel : ViewModelBase
    {
        private string result = null;

        private Visibility _progressVisibility = Visibility.Collapsed;
        public Visibility ProgressVisibility
        { get { return _progressVisibility; } set { Set(() => ProgressVisibility, ref _progressVisibility, value); } }

        private CodeRadarItem _selected;
        private CodeRadarItem _current;
        public object Selected
        {
            get { return _selected; }
            set
            {
                var message = value as CodeRadarItem;
                Set(ref _selected, message);
            }
        }

        public object Current
        {
            get { return _current; }
            set
            {
                var message = value as CodeRadarItem;
                Set(ref _current, message);
            }
        }


        private ObservableCollection<CodeRadarItem> item = new ObservableCollection<CodeRadarItem>();
        public ObservableCollection<CodeRadarItem> Item { get { return item; } set { item = value; } }

        private ObservableCollection<CodeRadarItem> curentItem = new ObservableCollection<CodeRadarItem>();
        public ObservableCollection<CodeRadarItem> CurrentItem { get { return curentItem; } set { curentItem = value; } }

        private ObservableCollection<CodeRadarItem> temp = new ObservableCollection<CodeRadarItem>();
        public ObservableCollection<CodeRadarItem> Temp { get { return temp; } set { temp = value; } }

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


        public async void getinfo()
        {
            Item.Clear();
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                ProgressVisibility = Visibility.Visible;
                string uristring = "https://www.hackerrank.com/calendar/feed.json";
                result = null;

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
                string start = null;
                string end = null;
                string link = null;
                Uri logo = null;
                SolidColorBrush brush = null;
                 



                JsonObject CodeFeed = JsonObject.Parse(result);
                JsonArray CodeFeedArray = CodeFeed.GetNamedArray("models");

                int len = CodeFeedArray.Count;
                for (uint i = 0;i<len ; i++)
                {

                    if (CodeFeedArray.GetObjectAt(i).ContainsKey("title"))
                        title = CodeFeedArray.GetObjectAt(i).GetNamedString("title");
                    else
                        title = null;

                    if (CodeFeedArray.GetObjectAt(i).ContainsKey("description"))
                        description = CodeFeedArray.GetObjectAt(i).GetNamedString("description");
                    else
                        description = null;

                    if (CodeFeedArray.GetObjectAt(i).ContainsKey("start"))
                        start = CodeFeedArray.GetObjectAt(i).GetNamedString("start");
                    else
                        start = null;

                    if (CodeFeedArray.GetObjectAt(i).ContainsKey("end"))
                        end = CodeFeedArray.GetObjectAt(i).GetNamedString("end");
                    else
                        end = null;

                    if (CodeFeedArray.GetObjectAt(i).ContainsKey("url"))
                        link = CodeFeedArray.GetObjectAt(i).GetNamedString("url");
                    else
                        link = null;
                    DateTime dtstart = Convert.ToDateTime(start);
                    DateTime dtend = Convert.ToDateTime(end);
                    DateTime now = DateTime.Now;
                    if (link.Contains("topcoder"))
                    {
                        logo = new Uri("ms-appx:///Assets/CodeRadarLogo/topcoder_logo.png");
                        brush = Application.Current.Resources["topcoder"] as SolidColorBrush;
                    }
                    else if (link.Contains("hackerrank"))
                    {
                        logo = new Uri("ms-appx:///Assets/CodeRadarLogo/hackerearth_logo.png");
                        brush = Application.Current.Resources["hackerrank"] as SolidColorBrush;
                    }
                    else if (link.Contains("codechef"))
                    {
                        logo = new Uri("ms-appx:///Assets/CodeRadarLogo/codechef_logo.png");
                        brush = Application.Current.Resources["codechef"] as SolidColorBrush;
                    }
                    else if (link.Contains("codeforces"))
                    {
                        logo = new Uri("ms-appx:///Assets/CodeRadarLogo/codeforces_logo.png");
                        brush = Application.Current.Resources["codeforces"] as SolidColorBrush;
                    }
                    else if (link.Contains("urionlinejudge"))
                    {
                        logo = new Uri("ms-appx:///Assets/CodeRadarLogo/uri_logo.png");
                        brush = Application.Current.Resources["urioj"] as SolidColorBrush;
                    }
                    else if (link.Contains("hackerearth"))
                    {
                        logo = new Uri("ms-appx:///Assets/CodeRadarLogo/hackerearth_logo.png");
                        brush = Application.Current.Resources["hackerearth"] as SolidColorBrush;
                    }else 
                    {
                        logo = new Uri("ms-appx:///Assets/CodeRadarLogo/unknown_logo.png");
                        brush = new SolidColorBrush(Colors.BlanchedAlmond);
                    }
                    if(now <=dtend && now >= dtstart)
                    Item.Add(new CodeRadarItem() {Days = "right now" , Start = dtstart.ToString("d MMM , yyy"), End = dtend.ToString("d MMM , yyy"), Description = description, Title = title,Link = link ,Logo = logo ,Color = brush });
                    else
                    CurrentItem.Add(new CodeRadarItem() { Days = (int)(dtstart - now ).TotalDays + " days",Start = dtstart.ToString("d MMM , yyy"), End = dtend.ToString("d MMM , yyy"), Description = description, Title = title, Link = link, Logo = logo, Color = brush });
                

                }


            }
            for (int i = 0; i < CurrentItem.Count; i++)
                CurrentItem.Move(CurrentItem.Count - 1, i);
            ProgressVisibility = Visibility.Collapsed;
            
        }

    }
}
