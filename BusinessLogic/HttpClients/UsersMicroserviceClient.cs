using BusinessLogic.DTO;
using System.Net;
using System.Net.Http.Json;

namespace BusinessLogic.HttpClients
{
    public class UsersMicroserviceClient
    {
        private readonly HttpClient _httpClient;

        public UsersMicroserviceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserResponse?> GetUserById(Guid id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/users/{id}");

            if (!response.IsSuccessStatusCode) {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return null;
                    case HttpStatusCode.BadRequest:
                        throw new HttpRequestException("Bad request", null, HttpStatusCode.BadRequest);
                    default:
                        throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}.");
                }
            }

            UserResponse? userResponse = await response.Content.ReadFromJsonAsync<UserResponse>();

            if (userResponse is null)
            {
                throw new ArgumentException("Invalid user response!");
            }

            return userResponse;
        }
    }
}
