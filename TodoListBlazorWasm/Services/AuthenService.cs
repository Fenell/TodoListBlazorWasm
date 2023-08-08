using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using TodoList.ViewModel;
using TodoList.ViewModel.User;

namespace TodoListBlazorWasm.Services
{
	public class AuthenService:IAuthenService
	{
		private readonly HttpClient _client;
		private readonly ILocalStorageService _localStorage;
		private readonly AuthenticationStateProvider _authenticationStateProvider;

		public AuthenService(HttpClient client, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
		{
			_client = client;
			_localStorage = localStorage;
			_authenticationStateProvider = authenticationStateProvider;
		}

		public async Task<Response<string>> Login(LoginRequest request)
		{
			var result = await _client.PostAsJsonAsync("/api/logins/login", request);
			var content = await result.Content.ReadAsStringAsync();
			var response = JsonConvert.DeserializeObject<Response<string>>(content);

			if (!result.IsSuccessStatusCode)
				return response;

			await _localStorage.SetItemAsync("authToken", response.Data);

			((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(request.UserName);
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", response.Data);

			return response;
		}

		public async Task Logout()
		{
			await _localStorage.RemoveItemAsync("authToken");
			((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();

			_client.DefaultRequestHeaders.Authorization = null;
		}
	}
}
