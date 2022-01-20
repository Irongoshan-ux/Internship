using CarMarket.Core.Car.Domain;
using CarMarket.Core.DataResult;
using CarMarket.Core.User.Domain;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
namespace CarMarket.UI.Services.Car
{
    public class HttpCarService : IHttpCarService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpAccessTokenSetter _httpAccessTokenSetter;

        public HttpCarService(HttpClient httpClient, HttpAccessTokenSetter httpAccessTokenSetter)
        {
            _httpClient = httpClient;
            _httpAccessTokenSetter = httpAccessTokenSetter;
            _httpAccessTokenSetter.HttpClient = _httpClient;
        }

        public async Task<CarModel> CreateAsync(CarModel userModel)
        {
            await _httpAccessTokenSetter.AddAccessTokenAsync();

            var response = await _httpClient.PostAsJsonAsync("/api/Car/CreateCar", userModel);

            return response.Content.ReadFromJsonAsync<CarModel>().Result;
        }

        public async Task DeleteAsync(long carId)
        {
            await _httpAccessTokenSetter.AddAccessTokenAsync();

            await _httpClient.DeleteAsync("/api/Car/DeleteCar/" + carId);
        }


        public async Task<IEnumerable<CarModel>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<CarModel>>("/api/Car/GetCars");
        }

        public async Task<IEnumerable<CarModel>> GetAllUserCarsByTokenAsync()
        {
            await _httpAccessTokenSetter.AddAccessTokenAsync();

            return await _httpClient.GetFromJsonAsync<IEnumerable<CarModel>>("/api/Car/GetAllUserCars");
        }

        public async Task<CarModel> GetAsync(long id)
        {
            return await _httpClient.GetFromJsonAsync<CarModel>("/api/Car/GetCar/" + id);
        }

        public async Task<DataResult<CarModel>> GetByPageAsync(int skip, int take)
        {
            return await _httpClient.GetFromJsonAsync<DataResult<CarModel>>($"/api/Car/GetCarsByPage?skip={skip}&take={take}");
        }

        public async Task<IEnumerable<CarModel>> SearchAsync(string carName, CarType? carType)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<CarModel>>($"/api/Car/Search?carName={carName}&carType={carType}");
            }
            catch
            {
                return null;
            }
            
        }

        public async Task UpdateAsync(long carId, CarModel car)
        {
            await _httpAccessTokenSetter.AddAccessTokenAsync();

            await _httpClient.PutAsJsonAsync("/api/Car/UpdateCar/" + carId, car);
        }
    }
}
