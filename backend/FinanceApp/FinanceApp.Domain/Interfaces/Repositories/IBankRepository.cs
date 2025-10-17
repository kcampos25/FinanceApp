using FinanceApp.Domain.Entities;
using FinanceApp.Domain.ListItem;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceApp.Domain.Interfaces.Repositories
{
    public interface IBankRepository
    {
        Task<BankEntity> GetByIdAsync(int id);
        Task<IEnumerable<BankEntity>> GetAllAsync();
        Task<BankEntity> AddAsync(BankEntity bank);
        Task UpdateAsync(BankEntity bank);
        Task DeleteAsync(int id);
        Task<IEnumerable<ListItemEntity>> GetBankLookupAsync();
    }
}
