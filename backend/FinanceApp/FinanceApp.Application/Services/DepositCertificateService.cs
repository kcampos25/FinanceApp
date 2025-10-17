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
    public class DepositCertificateService: IDepositCertificateService
    {
        private readonly IDepositCertificateRepository _certificateRepository;
        private readonly IBankRepository _bankRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper _mapper;

        public DepositCertificateService(IDepositCertificateRepository certificateRepository, IBankRepository bankRepository , ICurrencyRepository currencyRepository,IMapper mapper)
        {
            _certificateRepository = certificateRepository;
            _bankRepository = bankRepository;
            _currencyRepository = currencyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepositCertificateDTO>> GetAllAsync()
        {
            var certificate = await _certificateRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DepositCertificateDTO>>(certificate);
        }
        public async Task<DepositCertificateDTO> GetByIdAsync(int id)
        {
            var certificate = await _certificateRepository.GetByIdAsync(id);
            if (certificate == null) return null ;
            return _mapper.Map<DepositCertificateDTO>(certificate);
        }
        public async Task<DepositCertificateDTO> CreateAsync(CreateDepositCertificateDTO dto)
        {
            var certificate = new DepositCertificateEntity
            {
                BankId = dto.BankId,
                CurrencyId = dto.CurrencyId,
                Owner_name = dto.Owner_name,
                Description = dto.Description,
                Comment = dto.Comment,
                Amount = dto.Amount,
                Interest_amount = dto.Interest_amount,
                Start_date = dto.Start_date,
                Expiration_date = dto.Expiration_date,
                IsActive=dto.IsActive,
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.UtcNow
            };

            certificate.ValidateRules(); // reglas criticas de dominio

            await ValidateApplicationRules (certificate); // reglas de aplicacion

            var created = await _certificateRepository.AddAsync(certificate);
            return _mapper.Map<DepositCertificateDTO>(created);
        }
        public async Task UpdateAsync(int id, UpdateDepositCertificateDTO dto)
        {
            var existing = await _certificateRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Certificate with id {id} not found.");

            existing = _mapper.Map<DepositCertificateEntity>(dto);
            existing.CertificateId = id;
            existing.UpdatedAt = DateTime.Now;

            existing.ValidateRules(); // reglas criticas de dominio

            await ValidateApplicationRules(existing); // reglas de aplicacion

            await _certificateRepository.UpdateAsync(existing);
        }

        public async Task<IEnumerable<DepositCertificateReadOnlyViewDTO>> GetDetailAsync()
        {
            var certificate = await _certificateRepository.GetDetailAsync();
            return _mapper.Map<IEnumerable<DepositCertificateReadOnlyViewDTO>>(certificate);
        }

        public async Task DeleteDepositCertificateAsync(int certificateId)
        {
            var certificate = await _certificateRepository.GetByIdAsync(certificateId);

            if (certificate == null) throw new KeyNotFoundException($"Certificate with id {certificateId} not found.");

            await _certificateRepository.DeleteDepositCertificateAsync(certificateId);
        }

        private async Task ValidateApplicationRules(DepositCertificateEntity certification)
        {
            var bankExists = await _bankRepository.GetByIdAsync(certification.BankId);
            if (bankExists == null)
                throw new KeyNotFoundException($"Bank with id {certification.BankId} not found.");

            var currencyExists = await _currencyRepository.GetByIdAsync(certification.CurrencyId);
            if (currencyExists == null)
                throw new KeyNotFoundException($"Currency with id {certification.CurrencyId} not found.");
        }


    }
}
