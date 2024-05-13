using Enact.Models.API;
using Enact.Models.API.JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Enact.Services
{
    public class ValorantClient
    {
        private HttpClient Client { get; set; }
        private List<RouteModel> Routes { get; set; }

        public enum Server
        {
            Europe,
            NorthAmerica,
            Brazil,
            AsiaPacific,
            LatinaAmerica,
            Korea
        }

        private Dictionary<Server, Tuple<string, string>> ServerConfiguration { get; set; }
        private Server SelectedServer { get; set; }

        private string ApiKey = "RGAPI-91b2205b-0cf6-43c7-a758-1fe73fb887a8";

        public ValorantClient(Server server)
        {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Add("Accept", "application/json");

            SelectedServer = server;

            LoadServerConfiguration();
            LoadRoutes();
        }

        private void LoadServerConfiguration()
        {
            ServerConfiguration = new Dictionary<Server, Tuple<string, string>>()
            {
                { Server.Europe, new Tuple<string, string>("eu", "europe") },
                { Server.NorthAmerica, new Tuple<string, string>("na", "americas") },
                { Server.Brazil, new Tuple<string, string>("br", "americas") },
                { Server.AsiaPacific, new Tuple<string, string>("ap", "asia") },
                { Server.LatinaAmerica, new Tuple<string, string>("latam", "americas") },
                { Server.Korea, new Tuple<string, string>("kr", "asia") }
            };
        }

        private void LoadRoutes()
        {
            Routes = new List<RouteModel>()
            {
                new RouteModel(
                    RouteModel.Methods.GET,
                    "status",
                    new Uri("val/status/v1/platform-data", UriKind.Relative)
                ),
                new RouteModel(
                    RouteModel.Methods.GET,
                    "search",
                    new Uri("riot/account/v1/accounts/by-riot-id", UriKind.Relative)
                ),
                new RouteModel(
                    RouteModel.Methods.GET,
                    "matches",
                    new Uri("val/match/v1/matchlists/by-puuid/", UriKind.Relative)
                )
            };
        }

        private Uri BuildValorantUri(string entrypoint)
        {
            return (new Uri($"https://{ServerConfiguration[SelectedServer].Item1}.api.riotgames.com/{entrypoint}?api_key={ApiKey}"));
        }

        private Uri BuildRiotUri(string entrypoint)
        {
            return (new Uri($"https://{ServerConfiguration[SelectedServer].Item2}.api.riotgames.com/{entrypoint}?api_key={ApiKey}"));
        }

        private async Task<string?> Execute(Uri uri)
        {
            HttpResponseMessage response = await Client.GetAsync(uri.OriginalString);

            if (response.IsSuccessStatusCode == true)
            {
                using (Stream stream = response.Content.ReadAsStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return await reader.ReadToEndAsync();
                    }
                }
            }

            return (null);
        }

        private T? BuildOrNull<T>(string? response)
        {
            if (response != null)
            {
                return (JsonConvert.DeserializeObject<T>(response));
            }

            return default(T);
        }

        public async Task<StatusModel?> Status()
        {
            RouteModel route = Routes.First(x => x.Name == "status");
            string? response = await Execute(BuildValorantUri(route.Uri.OriginalString));

            return (BuildOrNull<StatusModel>(response));
        }

        public async Task<SearchModel?> Search(string name, string tag)
        {
            RouteModel route = Routes.First(x => x.Name == "search");
            string? response = await Execute(BuildRiotUri($"{route.Uri}/{name}/{tag}"));

            return (BuildOrNull<SearchModel>(response));
        }

        public async Task<SearchModel?> Matches(string id)
        {
            RouteModel route = Routes.First(x => x.Name == "matches");
            string? response = await Execute(BuildRiotUri($"{route.Uri}/{id}"));

            return (BuildOrNull<SearchModel>(response));
        }
    }
}
