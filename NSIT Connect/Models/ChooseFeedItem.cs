using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSIT_Connect.Models
{
    public class ChooseFeedItem : INotifyPropertyChanged
    {
        private Uri imagesource;
        private string title;
        private string id;

        public event PropertyChangedEventHandler PropertyChanged;

        public Uri ImageSource
        { get { return imagesource; } set { imagesource = value; OnPropertyChanged(); } }

        public string Title
        { get { return title; } set { title = value; OnPropertyChanged(); } }

        public string ID
        { get { return id; } set { id = value; OnPropertyChanged(); } }

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
