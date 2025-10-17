using FinanceApp.Domain.Entities;
using FinanceApp.Domain.ListItem;
using FinanceApp.Domain.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceApp.Domain.Interfaces
{
    public interface ICardRepository
    {
        Task<CardEntity> GetByIdAsync(int id);
        Task<IEnumerable<CardEntity>> GetAllAsync();
        Task<IEnumerable<CardReadOnlyView>> GetDetailAsync();
        Task<CardEntity> AddAsync(CardEntity bank);
        Task UpdateAsync(CardEntity bank);
        Task DeleteAsync(int id);
        Task<IEnumerable<ListItemEntity>> GetCardTypeLookupAsync();
    }
}
