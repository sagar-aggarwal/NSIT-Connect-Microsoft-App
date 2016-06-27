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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace NSIT_Connect.Controls
{
    public sealed partial class HangoutPanel : UserControl
    {
        public HangoutPanel()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("HangoutName", typeof(string), typeof(HangoutPanel), null);
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("ImageUri", typeof(Uri), typeof(HangoutPanel), null);
        public static readonly DependencyProperty VicinityProperty = DependencyProperty.Register("HangoutVicinity", typeof(string), typeof(HangoutPanel), null);
        public static readonly DependencyProperty AvailableProperty = DependencyProperty.Register("Available", typeof(string), typeof(HangoutPanel), null);
        public static readonly DependencyProperty RatingProperty = DependencyProperty.Register("RatValue", typeof(double), typeof(HangoutPanel), null);

        public string PlaceName
        {
            get { return (String)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public Uri Source
        {
            get { return (Uri)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public string PlaceVicinity
        {
            get { return (string)GetValue(VicinityProperty); }
            set { SetValue(VicinityProperty, value); }
        }
        public string PlaceAvailable
        {
            get { return (string)GetValue(AvailableProperty); }
            set { SetValue(AvailableProperty, value); }
        }
        public double RatingValue
        {
            get { return (double)GetValue(RatingProperty); }
            set { SetValue(RatingProperty, value); }
        }


    }
}
