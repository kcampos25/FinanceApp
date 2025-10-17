using FinanceApp.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceApp.Application.Interfaces
{
    public interface ICurrencyService
    {
        Task<IEnumerable<CurrencyDTO>> GetAllAsync();
        Task<CurrencyDTO> GetByIdAsync(int id);
        Task<CurrencyDTO> CreateAsync(CreateCurrencyDTO dto);
        Task UpdateAsync(int id, UpdateCurrencyDTO dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<ListItemDTO>> GetCurrencyLookupAsync();
    }
}
