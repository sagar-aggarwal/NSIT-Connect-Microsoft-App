using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSIT_Connect.Models
{
    public class YVideoItem : INotifyPropertyChanged
    {
        private string title;
        private string description;
        private string date;
        private string videoid;
        private Uri thumnail;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Title
        { get { return title; } set { title = value; OnPropertyChanged(); } }

        public string Descrption
        { get { return description; } set { description = value; OnPropertyChanged(); } }

        public string Date
        { get { return date; } set { date = value; OnPropertyChanged(); } }

        public string VideoID
        { get { return videoid; } set { videoid = value; OnPropertyChanged(); } }

        public Uri Thumbnail
        { get { return thumnail; } set { thumnail = value; OnPropertyChanged(); } }

        protected void OnPropertyChanged([CallerMemberName]string name = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
