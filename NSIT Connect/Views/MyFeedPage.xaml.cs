using NSIT_Connect.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
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
    public sealed partial class MyFeedPage : Page
    {
        public MyFeedPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        
        }

        private void NarrowVisualStateGroup_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            var currentstate = e.NewState.Name;
            if (currentstate == "DetailsVisualState")
                Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            else
                Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;
            if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
            {
                ViewModel.getinfo(Constants.id_nsitonline);
            }
        }

        private void MailList_ItemClick(object sender, ItemClickEventArgs e)
        {
            ViewModel.getpicture(e.ClickedItem as Feed);
        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            var url = (ViewModel.Selected as Feed).PictureUri;
            if (url != null)
            {
                string filename = "Image_" + (ViewModel.Selected as Feed).Object_ID + ".jpg";
                StorageFolder picsFolder = KnownFolders.SavedPictures;
                StorageFile file = await picsFolder.CreateFileAsync(filename, CreationCollisionOption.GenerateUniqueName);

                HttpClient client = new HttpClient();

                byte[] responseBytes = await client.GetByteArrayAsync(url);

                var stream = await file.OpenAsync(FileAccessMode.ReadWrite);

                using (var outputStream = stream.GetOutputStreamAt(0))
                {
                    DataWriter writer = new DataWriter(outputStream);
                    writer.WriteBytes(responseBytes);
                    await writer.StoreAsync();
                    await outputStream.FlushAsync();
                }

                var mssg = new MessageDialog("Your Picture Is Saved in Folder :" + file.Path);
                await mssg.ShowAsync();

            }
            else
            {
                var mssg = new MessageDialog("No Internet Connection");
                await mssg.ShowAsync();
            }

        }

        private async void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            var uri = new Uri((ViewModel.Selected as Feed).Link);
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

        private  void Flyout_Closed(object sender, object e)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            for (int i = 0; i < ViewModel.Item.Count; i++)
            {
                ChooseFeedItem item = ViewModel.Item[i];
                if (Choose_GridView.SelectedItems.Contains(item))
                {
                     localSettings.Values[item.Title] = true;
                }
                else
                {
                    localSettings.Values[item.Title] = false;
                }
            }  
        }

        private  void Choose_GridView_Loaded(object sender, RoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            for (int i = 0; i < ViewModel.Item.Count; i++)
            {
                ChooseFeedItem item = ViewModel.Item[i];
                if (localSettings.Values[item.Title] != null)
                {
                    bool selected = (bool)localSettings.Values[item.Title];
                    if (selected)
                    {
                        Choose_GridView.SelectedItems.Add(item);

                    }
                }
            }

        }
    }
}
