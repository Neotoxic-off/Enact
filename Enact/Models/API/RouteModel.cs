using Enact.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Enact.Models.API
{
    public class RouteModel : INotified
    {
        public enum Methods
        {
            GET,
            POST,
            PUT,
            DELETE,
            PATCH
        }

        private Methods _method;
        public Methods Method
        {
            get { return _method; }
            set { SetProperty(ref _method, value); }
        }

        private Uri _uri;
        public Uri Uri
        {
            get { return _uri; }
            set { SetProperty(ref _uri, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public RouteModel(Methods method, string name, Uri uri)
        {
            Method = method;
            Name = name;
            Uri = uri;
        }
    }
}
