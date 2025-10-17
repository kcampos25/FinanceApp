using FinanceApp.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceApp.Application.Interfaces
{
    public interface IBankService
    {
        Task<IEnumerable<BankDTO>> GetAllAsync();
        Task<BankDTO> GetByIdAsync(int id);
        Task<BankDTO> CreateAsync(CreateBankDTO dto);
        Task UpdateAsync(int id, UpdateBankDTO dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<ListItemDTO>> GetBankLookupAsync();
    }
}
