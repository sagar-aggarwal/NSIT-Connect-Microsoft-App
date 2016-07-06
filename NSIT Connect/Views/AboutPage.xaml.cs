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
    public sealed partial class AboutPage : Page
    {
        public AboutPage()
        {
            this.InitializeComponent();
        }

        private async void RepoButto_Click(object sender, RoutedEventArgs e)
        {
            var uri = new Uri("https://github.com/sgaggarwal2009/NSIT-Connect-Microsoft-App");
            if (uri != null)
            {
                var success = await Windows.System.Launcher.LaunchUriAsync(uri);
            }
        }

        private async void ContributeButto_Click(object sender, RoutedEventArgs e)
        {
            var uri = new Uri("https://github.com/sgaggarwal2009/NSIT-Connect-Microsoft-App");
            if (uri != null)
            {
                var success = await Windows.System.Launcher.LaunchUriAsync(uri);
            }

        }

        private async void LinkedinButton_Click(object sender, RoutedEventArgs e)
        {
            var uri = new Uri("https://in.linkedin.com/in/sagar-aggarwal-9319b3110");
            if (uri != null)
            {
                var success = await Windows.System.Launcher.LaunchUriAsync(uri);
            }

        }

        private async void FacebookButton_Click(object sender, RoutedEventArgs e)
        {
            var uri = new Uri("https://web.facebook.com/sagar.aggarwal.773");
            if (uri != null)
            {
                var success = await Windows.System.Launcher.LaunchUriAsync(uri);
            }

        }

        private async void GithubButton_Click(object sender, RoutedEventArgs e)
        {
            var uri = new Uri("https://github.com/sgaggarwal2009");
            if (uri != null)
            {
                var success = await Windows.System.Launcher.LaunchUriAsync(uri);
            }
        }
    }
}
