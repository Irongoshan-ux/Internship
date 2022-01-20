using CarMarket.Core.Car.Domain;
using CarMarket.Core.DataResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.Core.Car.Repository
{
    public interface ICarRepository
    {
        Task<CarModel> FindByIdAsync(long id);
        Task<CarModel> AddAsync(CarModel car);
        Task<IEnumerable<CarModel>> FindAllAsync();
        Task DeleteAsync(long carId);
        Task<CarModel> UpdateAsync(long carId, CarModel car);
        Task<IEnumerable<CarModel>> FindAllUserCarsAsync(string userId);
        Task<IEnumerable<CarModel>> SearchAsync(string carName, CarType? carType);
        Task<DataResult<CarModel>> FindByPageAsync(int skip = 0, int take = 5);
    }
}