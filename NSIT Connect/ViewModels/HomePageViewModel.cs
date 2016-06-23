using NSIT_Connect.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Mvvm;
using Windows.Data.Json;
using Windows.UI.Xaml.Navigation;

namespace NSIT_Connect.ViewModels
{
    class HomePageViewModel : ViewModelBase
    {
        private string next = "https://graph.facebook.com/" + Constants.id_nsitonline + "/posts?limit=20&fields=id,picture,from,shares,message," +
            "object_id,link,created_time,comments.limit(0).summary(true),likes.limit(0).summary(true)" +
            "&access_token=" + Constants.common_access;
        private string result = null;


        private string hobject, hmessage, hpicture, hlink, hlikes, htime;

        public HomePageViewModel()
        {
            HomeFeed = new ObservableCollection<Feed>();
          
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            RefreshCommand.Execute();
            return Task.CompletedTask;
        }

        ObservableCollection<Feed> _homefeed = default(ObservableCollection<Feed>);

        public ObservableCollection<Feed> HomeFeed
        {
            get { return _homefeed; }
            private set { Set(ref _homefeed, value); }
        }

        //string _searchText = default(string);

        //public string SearchText
        //{
        //    get { return _searchText; }
        //    set { Set(ref _searchText, value); }
        //}

        //public readonly DelegateCommand SwitchToPageCommand =
        //    new DelegateCommand(() => BootStrapper.Current.NavigationService.Navigate(typeof(MainPage)));

        public DelegateCommand RefreshCommand => new DelegateCommand(() =>
        {
            IsMasterLoading = true;
            HomeFeed.Clear();
            Selected = null;
            WindowWrapper.Current().Dispatcher.Dispatch(() =>
            {
                getinfo();
                Selected = HomeFeed?.FirstOrDefault();
                IsMasterLoading = false;
                
            }, 2000);
        });

        Feed _selected = default(Feed);

        public object Selected
        {
            get { return _selected; }
            set
            {
                var message = value as Feed;
                Set(ref _selected, message);
                NextCommand.RaiseCanExecuteChanged();
                PreviousCommand.RaiseCanExecuteChanged();
                if (message == null) return;
                message.IsRead = true;
                IsDetailsLoading = true;

                WindowWrapper.Current().Dispatcher.Dispatch(() =>
                {
                    IsDetailsLoading = false;
                }, 1000);
            }
        }

        private DelegateCommand _nextCommand;

        public DelegateCommand NextCommand
        {
            get
            {
                return _nextCommand ??
                    (_nextCommand = new DelegateCommand(ExecuteNextCommand, CanExecuteNextCommand));
            }
            set { Set(ref _nextCommand, value); }
        }

        private void ExecuteNextCommand()
        {
            if (Selected == null)
                return;
            var index = HomeFeed.IndexOf(_selected);
            if (index == -1)
                return;
            var next = index + 1;
            Selected = HomeFeed[next];
        }

        private bool CanExecuteNextCommand()
        {
            if (Selected == null)
                return false;
            var index = HomeFeed.IndexOf(_selected);
            if (index == -1)
                return false;
            return index < (HomeFeed.Count - 1);
        }

        private DelegateCommand _previousCommand;

        public DelegateCommand PreviousCommand
        {
            get
            {
                return _previousCommand ??
                       (_previousCommand = new DelegateCommand(ExecutePreviousCommand, CanExecutePreviousCommand));
            }
            set { Set(ref _previousCommand, value); }
        }

        private bool CanExecutePreviousCommand()
        {
            if (Selected == null)
                return false;
            var index = HomeFeed.IndexOf(_selected);
            return index > 0;
        }

        private void ExecutePreviousCommand()
        {
            if (Selected == null)
                return;
            var index = HomeFeed.IndexOf(_selected);
            if (index == -1)
                return;
            var previous = index - 1;
            Selected = HomeFeed[previous];
        }

        private bool _isDetailsLoading;

        public bool IsDetailsLoading
        {
            get { return _isDetailsLoading; }
            set { Set(ref _isDetailsLoading, value); }
        }

        private bool _isMasterLoading;

        public bool IsMasterLoading
        {
            get { return _isMasterLoading; }
            set { Set(ref _isMasterLoading, value); }
        }

        public async void getinfo()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                var httpClient = new HttpClient();
                var uri = new Uri(next);

                try
                {
                    HttpResponseMessage responseMessage = await httpClient.GetAsync(uri);
                    responseMessage.EnsureSuccessStatusCode();
                    result = await responseMessage.Content.ReadAsStringAsync();
                }
                catch (Exception ex) { }


                httpClient.Dispose();
            }
            JsonObject ob, ob2;
            JsonArray arr;
            if (result != null && result!= string.Empty)
            {
                ob = JsonObject.Parse(result);
                arr = ob.GetNamedArray("data");
                

                int len = arr.Count;
                for (uint i = 0; i < len; i++)
                {

                    ob2 = arr.GetObjectAt(i).GetNamedObject("from");
                    string s2 = ob2.GetNamedString("id");
                    if (!s2.Equals(Constants.id_nsitonline))
                        continue;

                    if (arr.GetObjectAt(i).ContainsKey("message"))
                        hmessage = arr.GetObjectAt(i).GetNamedString("message");
                    else
                        hmessage = string.Empty;

                    if (!(arr.GetObjectAt(i).ContainsKey("object_id")))
                        hobject = string.Empty;
                    else
                        hobject = arr.GetObjectAt(i).GetNamedString("object_id");

                    if (arr.GetObjectAt(i).ContainsKey("picture"))
                    {
                        hpicture = arr.GetObjectAt(i).GetNamedString("picture");
                    }
                    else
                        hpicture = string.Empty;
                    if (arr.GetObjectAt(i).ContainsKey("link"))
                    {
                        hlink = arr.GetObjectAt(i).GetNamedString("link");
                    }
                    else
                        hlink = string.Empty;
                    if (arr.GetObjectAt(i).ContainsKey("likes"))
                    {

                        JsonObject o = arr.GetObjectAt(i).GetNamedObject("likes");
                        JsonObject o2 = o.GetNamedObject("summary");

                        hlikes = o2.GetNamedNumber("total_count").ToString();   //No of likes
                    }
                    else
                        hlikes = "0";

                    if (arr.GetObjectAt(i).ContainsKey("created_time"))
                        htime = arr.GetObjectAt(i).GetNamedString("created_time");
                    else
                        htime = string.Empty;
                    HomeFeed.Add(new Feed() { Object_ID = hobject, Likes = hlikes, Link = hlink, Message = hmessage, Picture = hpicture, Time_Created = htime });

                }
                ob = ob.GetNamedObject("paging");
                next = ob.GetNamedString("next");

            }

        }
    }
}

