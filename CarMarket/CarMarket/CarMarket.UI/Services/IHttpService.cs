using CarMarket.Core.DataResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.UI.Services
{
    public interface IHttpService<TModel, TKey>
    {
        Task<TModel> GetAsync(TKey id);
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<DataResult<TModel>> GetByPageAsync(int skip, int take);
        Task<TModel> CreateAsync(TModel model);
        Task UpdateAsync(TKey id, TModel updatedModel);
        Task DeleteAsync(TKey Id);
    }
}
