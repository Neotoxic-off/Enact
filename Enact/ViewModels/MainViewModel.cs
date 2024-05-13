using Enact.Models.API.JSON;
using Enact.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Enact.ViewModels
{
    public class MainViewModel : Interfaces.INotified
    {
        private ValorantClient _valorantClient;
        public ValorantClient ValorantClient
        {
            get { return _valorantClient; }
            set { SetProperty(ref _valorantClient, value); }
        }

        private OAuth2 _oauth2;
        public OAuth2 OAuth2
        {
            get { return _oauth2; }
            set { SetProperty(ref _oauth2, value); }
        }
        

        private StatusModel _status;
        public StatusModel Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }

        private string _searchPlayerName;
        public string SearchPlayerName
        {
            get { return _searchPlayerName; }
            set { SetProperty(ref _searchPlayerName, value); }
        }

        private string _searchPlayerTag;
        public string SearchPlayerTag
        {
            get { return _searchPlayerTag; }
            set { SetProperty(ref _searchPlayerTag, value); }
        }

        private SearchModel _searchedPlayer;
        public SearchModel SearchedPlayer
        {
            get { return _searchedPlayer; }
            set { SetProperty(ref _searchedPlayer, value); }
        }

        private AsyncDelegateCommand _searchPlayerCommand;
        public AsyncDelegateCommand SearchPlayerCommand
        {
            get { return _searchPlayerCommand; }
            set { SetProperty(ref _searchPlayerCommand, value); }
        }

        public MainViewModel()
        {
            Status = new StatusModel();
            SearchedPlayer = new SearchModel();

            SearchPlayerName = null;
            SearchPlayerTag = null;

            SearchPlayerCommand = new AsyncDelegateCommand(SearchPlayer);

            //OAuth2 = new OAuth2(3773);

            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            ValorantClient = new ValorantClient(ValorantClient.Server.Europe);

            await LoadStatus(null);
        }
        private async Task LoadStatus(object data)
        {
            Status = await ValorantClient.Status();
        }

        private async Task SearchPlayer(object data)
        {
            SearchedPlayer = await ValorantClient.Search(SearchPlayerName, SearchPlayerTag);
        }

        private async Task LoadMatches(object data)
        {
            await ValorantClient.Matches(SearchedPlayer.puuid);
        }
    }
}
