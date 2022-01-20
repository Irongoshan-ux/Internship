using CarMarket.Core.DataResult;
using CarMarket.Core.User.Domain;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CarMarket.UI.Services.User
{
    public class HttpUserService : IHttpUserService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpAccessTokenSetter _httpAccessTokenSetter;

        public HttpUserService(HttpClient httpClient, HttpAccessTokenSetter httpAccessTokenSetter)
        {
            _httpClient = httpClient;
            _httpAccessTokenSetter = httpAccessTokenSetter;
            _httpAccessTokenSetter.HttpClient = _httpClient;
        }

        public async Task AddPermissionAsync(string userId, params Permission[] permissions)
        {
            await _httpAccessTokenSetter.AddAccessTokenAsync();

            await _httpClient.PostAsJsonAsync($"/api/User/ChangeUserPermission?userId={userId}", permissions);
        }

        public async Task ChangePermissionAsync(string userId, Permission replaceablePermission, Permission substitutePermission)
        {
            await _httpAccessTokenSetter.AddAccessTokenAsync();

            await _httpClient.PostAsJsonAsync($"/api/User/ChangeUserPermission?userId={userId}&replaceablePermission={replaceablePermission}&substitutePermission={substitutePermission}", userId);
        }

        public async Task<UserModel> CreateAsync(UserModel model)
        {
            await _httpAccessTokenSetter.AddAccessTokenAsync();

            var response = await _httpClient.PostAsJsonAsync("/api/User/CreateUser", model);

            return response.Content.ReadFromJsonAsync<UserModel>().Result;
        }

        public async Task DeleteAsync(string id)
        {
            await _httpAccessTokenSetter.AddAccessTokenAsync();

            await _httpClient.DeleteAsync($"/api/User/DeleteUser/{id}");
        }

        public async Task DeletePermissionAsync(string userId, Permission permission)
        {
            await _httpAccessTokenSetter.AddAccessTokenAsync();

            await _httpClient.DeleteAsync($"/api/User/DeleteUserPermission/{userId}");
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<UserModel>>($"/api/User/GetUsers");
        }

        public async Task<UserModel> GetAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<UserModel>($"/api/User/GetUser/{id}");
        }

        public async Task<UserModel> GetByEmailAsync(string email)
        {
            return await _httpClient.GetFromJsonAsync<UserModel>($"/api/User/GetByEmail?email={email}");
        }

        public async Task<DataResult<UserModel>> GetByPageAsync(int skip, int take)
        {
            return await _httpClient.GetFromJsonAsync<DataResult<UserModel>>($"/api/User/GetUsersByPage?skip={skip}&take={take}");
        }

        public async Task UpdateAsync(string id, UserModel updatedModel)
        {
            await _httpAccessTokenSetter.AddAccessTokenAsync();

            await _httpClient.PutAsJsonAsync("/api/User/UpdateUser/" + id, updatedModel);
        }
    }
}
