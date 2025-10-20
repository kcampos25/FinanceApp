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
    public class BankService : IBankService
    {
        private readonly IBankRepository _bankRepository;
        private readonly IMapper _mapper;

        public BankService(IBankRepository bankRepository, IMapper mapper)
        {
            _bankRepository = bankRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BankDTO>> GetAllAsync()
        {
            var banks = await _bankRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BankDTO>>(banks);
        }
        public async Task<BankDTO> GetByIdAsync(int id)
        {
            var bank = await _bankRepository.GetByIdAsync(id);
            if (bank == null) return null;
            return _mapper.Map<BankDTO>(bank);
        }
        public async Task<BankDTO> CreateAsync(CreateBankDTO dto)
        {
            var bank = new BankEntity
            {
                Description = dto.Description,
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.UtcNow
            };
            bank.ValidateForCreate();
            var created = await _bankRepository.AddAsync(bank);
            return _mapper.Map<BankDTO>(created);
        }
        public async Task UpdateAsync(int id, UpdateBankDTO dto)
        {
            var existing = await _bankRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Bank with id {id} not found.");
            existing.Update(dto.Description, dto.UpdatedBy);
            await _bankRepository.UpdateAsync(existing);
        }
        public async Task DeleteAsync(int id)
        {
            var existing = await _bankRepository.GetByIdAsync(id);
            if (existing == null)

                throw new KeyNotFoundException($"Bank with id {id} not found.");
            await _bankRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ListItemDTO>> GetBankLookupAsync()
        {
            var banks = await _bankRepository.GetBankLookupAsync();
            return _mapper.Map<IEnumerable<ListItemDTO>>(banks);
        }
    }
}
