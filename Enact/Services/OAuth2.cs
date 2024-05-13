using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Enact.Services
{
    public class OAuth2 : Interfaces.INotified
    {
        private int _port;
        public int Port
        {
            get { return _port; }
            set { SetProperty(ref _port, value); }
        }

        private string _uri;
        public string Uri
        {
            get { return _uri; }
            set { SetProperty(ref _uri, value); }
        }

        public OAuth2(int port)
        {
            int client_id = 689589;
            Uri = $"http://localhost:{port}/";
            Port = port;

            Process.Start(
                new ProcessStartInfo($"https://auth.riotgames.com/authorize?client_id={client_id}&redirect_uri={Uri}&response_type=code&scope=openid+offline_access")
                {
                    UseShellExecute = true
                }
            );
        }

        public void Run()
        {
            HttpListener listener = new HttpListener();

            listener.Prefixes.Add(Uri);
            listener.Start();

            Task.Run(async () =>
            {
                var context = await listener.GetContextAsync();
                var request = context.Request;

                string authorizationCode = request.QueryString.Get("code");

                listener.Close();

                ProcessAuthorizationCode(authorizationCode);
            });
        }

        private void ProcessAuthorizationCode(string authorizationCode)
        {
        }
    }
}
