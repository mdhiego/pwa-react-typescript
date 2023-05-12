using System.Net;
using System.Net.Http.Json;
using BabySounds.Contracts.Responses;
using Microsoft.AspNetCore.Components;

namespace BabySounds.Client.Services
{
    public sealed class BabySoundsWebClient
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public BabySoundsWebClient(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        internal async Task<List<TrackResponse>?> GetTracks()
        {
            using var response = await _httpClient.GetAsync("/tracks");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("login");
            }

            var result = await _httpClient.GetFromJsonAsync<List<TrackResponse>>("/tracks");

            return result;
        }

        internal async Task<List<TrackResponse>?> GetPlaylists()
        {
            using var response = await _httpClient.GetAsync("/playlists");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("login");
            }

            var result = await _httpClient.GetFromJsonAsync<List<TrackResponse>>("/playlists");

            return result;
        }
    }
}
