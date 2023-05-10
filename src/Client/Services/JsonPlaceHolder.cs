using System.Net.Http.Json;
using BabySounds.Contracts.Responses;
using Microsoft.AspNetCore.Components;

namespace BabySounds.Client.Services
{
    public sealed class JsonPlaceHolder
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public JsonPlaceHolder(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        internal async Task<List<TrackResponse>?> GetTracks()
        {
            using var response = await _httpClient.GetAsync("/tracks");

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("notauthorized");
            }

            var result = await _httpClient.GetFromJsonAsync<List<TrackResponse>>("/tracks");

            return result;
        }
    }
}
