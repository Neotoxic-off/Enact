using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Enact.Services
{
    public class ValorantClient
    {
        private HttpClient Client { get; set; }

        public ValorantClient()
        {
            Client = new HttpClient();
        }

        private async void Status()
        {
            var responseString = await Client.GetStreamAsync("http://www.example.com/recepticle.aspx");
        }
    }
}
