using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace NSIT_Connect.Models
{
    public class Feed : BindableBase
    {
        string _Message = default(string);
        public string Message { get { return _Message; } set { Set(ref _Message, value); } }

        string _Object_ID = default(string);
        public string Object_ID { get { return _Object_ID; } set { Set(ref _Object_ID, value); } }

        string _Likes = default(string);
        public string Likes { get { return _Likes; } set { Set(ref _Likes, value); } }

        string _Link = default(string);
        public string Link { get { return _Link; } set { Set(ref _Link, value); } }

        string _Time_Created = default(string);
        public string Time_Created { get { return _Time_Created; } set { Set(ref _Time_Created, value); } }

        string _Picture = default(string);
        public string Picture { get { return _Picture; } set { Set(ref _Picture, value); } }

        Uri _PictureUri = default(Uri);
        public Uri PictureUri { get { return _PictureUri; } set { Set(ref _PictureUri, value); } }

        DateTime _Date = default(DateTime);
        public DateTime Date { get { return _Date; } set { Set(ref _Date, value); } }

        bool _IsRead = false;
        public bool IsRead { get { return _IsRead; } set { Set(ref _IsRead, value); } }
    }
}
