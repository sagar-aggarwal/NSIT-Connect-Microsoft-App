using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSIT_Connect.Models
{
    class ReviewItem : INotifyPropertyChanged
    {
        private string name;
        private double rating;
        private string profile;
        private string text;
        private string time;

        public event PropertyChangedEventHandler PropertyChanged;

        public double Rating
        { get { return rating; } set { rating = value; OnPropertyChanged(); } }

        public string Time
        { get { return time; } set { time = value; OnPropertyChanged(); } }

        public string Name
        { get { return name; } set { name = value; OnPropertyChanged(); } }

        public string Profile
        { get { return profile; } set { profile = value; OnPropertyChanged(); } }

        public string Text
        { get { return text; } set { text = value; OnPropertyChanged(); } }

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
