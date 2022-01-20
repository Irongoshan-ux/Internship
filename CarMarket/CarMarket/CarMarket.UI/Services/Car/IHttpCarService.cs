using CarMarket.Core.Car.Domain;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CarMarket.UI.Services.Car
{
    public interface IHttpCarService : IHttpService<CarModel, long>
    {
        Task<IEnumerable<CarModel>> GetAllUserCarsByTokenAsync();
        Task<IEnumerable<CarModel>> SearchAsync(string carName, CarType? carType);
    }
}
