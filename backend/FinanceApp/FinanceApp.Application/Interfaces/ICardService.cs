using FinanceApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Application.Interfaces
{
    public interface ICardService
    {
        Task<IEnumerable<CardDTO>> GetAllAsync();
        Task<IEnumerable<CardReadOnlyViewDTO>> GetDetailAsync();
        Task<CardDTO> GetByIdAsync(int id);
        Task UpdateAsync(int id, UpdateCardDTO dto);
        Task<CardDTO> CreateAsync(CreateCardDTO dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<ListItemDTO>> GetCardTypeLookupAsync();
    }
}
