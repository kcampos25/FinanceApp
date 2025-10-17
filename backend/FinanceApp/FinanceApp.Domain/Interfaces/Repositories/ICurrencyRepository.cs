using FinanceApp.Domain.Entities;
using FinanceApp.Domain.ListItem;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceApp.Domain.Interfaces.Repositories
{
    public interface ICurrencyRepository
    {
        Task<CurrencyEntity> GetByIdAsync(int id);
        Task<IEnumerable<CurrencyEntity>> GetAllAsync();
        Task<CurrencyEntity> AddAsync(CurrencyEntity bank);
        Task UpdateAsync(CurrencyEntity bank);
        Task DeleteAsync(int id);
        Task<IEnumerable<ListItemEntity>> GetCurrencyLookupAsync();
    }
}
