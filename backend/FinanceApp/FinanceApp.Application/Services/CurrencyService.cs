using AutoMapper;
using FinanceApp.Application.DTOs;
using FinanceApp.Application.Interfaces;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceApp.Application.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _CurrencyRepository;
        private readonly IMapper _mapper;

        public CurrencyService(ICurrencyRepository CurrencyRepository, IMapper mapper)
        {
            _CurrencyRepository = CurrencyRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CurrencyDTO>> GetAllAsync()
        {
            var Currencys = await _CurrencyRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CurrencyDTO>>(Currencys);
        }
        public async Task<CurrencyDTO> GetByIdAsync(int id)
        {
            var Currency = await _CurrencyRepository.GetByIdAsync(id);
            if (Currency == null) return null;
            return _mapper.Map<CurrencyDTO>(Currency);
        }
        public async Task<CurrencyDTO> CreateAsync(CreateCurrencyDTO dto)
        {
            var Currency = new CurrencyEntity
            {
                Description = dto.Description,
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.UtcNow
            };
            Currency.ValidateForCreate();
            var created = await _CurrencyRepository.AddAsync(Currency);
            return _mapper.Map<CurrencyDTO>(created);
        }
        public async Task UpdateAsync(int id, UpdateCurrencyDTO dto)
        {
            var existing = await _CurrencyRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Currency with id {id} not found.");
            existing.Update(dto.Description, dto.UpdatedBy);
            await _CurrencyRepository.UpdateAsync(existing);
        }
        public async Task DeleteAsync(int id)
        {
            var existing = await _CurrencyRepository.GetByIdAsync(id);
            if (existing == null)

                throw new KeyNotFoundException($"Currency with id {id} not found.");
            await _CurrencyRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ListItemDTO>> GetCurrencyLookupAsync()
        {
            var currencies = await _CurrencyRepository.GetCurrencyLookupAsync();
            return _mapper.Map<IEnumerable<ListItemDTO>>(currencies);
        }
    }
}
