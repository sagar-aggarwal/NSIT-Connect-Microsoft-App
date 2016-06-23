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
    public sealed partial class CircularPanel : UserControl
    {
        public CircularPanel()
        {
            this.InitializeComponent();
        }
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("LabelName", typeof(String), typeof(CircularPanel), null);
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("ImageUri", typeof(Uri), typeof(CircularPanel), null);

        public String Label
        {
            get { return (String)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public Uri Source
        {
            get { return (Uri)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
    }
}
