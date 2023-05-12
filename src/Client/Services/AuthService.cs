using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using BabySounds.Contracts.Requests;
using BabySounds.Contracts.Responses;
using Blazored.LocalStorage;
using ErrorOr;
using Microsoft.AspNetCore.Components.Authorization;

namespace BabySounds.Client.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(
            HttpClient httpClient,
            AuthenticationStateProvider authenticationStateProvider,
            ILocalStorageService localStorage
        )
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<ErrorOr<RegisterResponse>> Register(RegisterRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/register", request);

            if (!response.IsSuccessStatusCode) return Error.Failure();

            var responseAsJson = JsonSerializer.Deserialize<RegisterResponse>(await response.Content.ReadAsStringAsync())!;

            return responseAsJson;
        }

        public async Task<ErrorOr<LoginResponse>> Login(LoginRequest request)
        {
            var requestAsJson = JsonSerializer.Serialize(request);

            var response = await _httpClient.PostAsync(
                "auth/login",
                new StringContent(requestAsJson, Encoding.UTF8, "application/json")
            );

            if (!response.IsSuccessStatusCode) return Error.Failure();

            var responseAsJson = JsonSerializer.Deserialize<LoginResponse>(await response.Content.ReadAsStringAsync())!;

            await _localStorage.SetItemAsync("access_token", responseAsJson.AccessToken);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(request.UserName);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", responseAsJson.AccessToken);

            return responseAsJson;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("access_token");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
