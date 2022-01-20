using CarMarket.Core.Car.Domain;
using CarMarket.Core.DataResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.Core.Car.Service
{
    public interface ICarService
    {
        Task<CarModel> CreateAsync(CarModel userModel);
        Task<CarModel> GetAsync(long id);
        Task<IEnumerable<CarModel>> GetAllAsync();
        Task DeleteAsync(long carId);
        Task<CarModel> UpdateCarAsync(long carId, CarModel car);
        Task<IEnumerable<CarModel>> GetAllUserCarsAsync(string userId);
        Task<IEnumerable<CarModel>> SearchAsync(string carName, CarType? carType);
        Task<DataResult<CarModel>> GetByPageAsync(int skip = 0, int take = 5);
    }
}
