using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Interfaces.Repositories;
using FinanceApp.Domain.Queries;
using FinanceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Repositories
{
    //Deposi tCertificate Repository
    public class DepositCertificateRepository : IDepositCertificateRepository
    {

        private readonly FinanceDbContext _context;
        public DepositCertificateRepository(FinanceDbContext context)
        {
            _context = context;
        }

        private DepositCertificateEntity MapToDomain(DepositCertificate e)
        {
            if (e == null) return null;
            return new DepositCertificateEntity
            {
                CertificateId = e.CertificateId,
                BankId = e.BankId,
                CurrencyId = e.CurrencyId,
                Owner_name = e.OwnerName,
                Description = e.Description,
                Comment = e.Comment,
                Amount = e.Amount,
                Interest_amount = e.InterestAmount,
                Start_date = e.StartDate,
                Expiration_date = e.ExpirationDate,
                IsActive =e.IsActive,
                CreatedBy = e.CreatedBy,
                CreatedAt = e.CreatedAt,
                UpdatedBy = e.UpdatedBy,
                UpdatedAt = e.UpdatedAt
            };
        }

        private void MapToEntity(DepositCertificateEntity domain, DepositCertificate data)
        {
            data.BankId = domain.BankId;
            data.CurrencyId = domain.CurrencyId;
            data.OwnerName = domain.Owner_name;
            data.Description = domain.Description;
            data.Comment = domain.Comment;
            data.Amount = domain.Amount;
            data.InterestAmount = domain.Interest_amount;
            data.StartDate = domain.Start_date;
            data.ExpirationDate = domain.Expiration_date;
            data.IsActive = domain.IsActive;

            if (domain.CertificateId > 0)
            {
                data.UpdatedBy = domain.UpdatedBy;
                data.UpdatedAt = domain.UpdatedAt;
            }
            else
            {
                data.CreatedBy = domain.CreatedBy;
                data.CreatedAt = domain.CreatedAt;
            }
        }

        public async Task<IEnumerable<DepositCertificateEntity>> GetAllAsync()
        {
            var list = await _context.DepositCertificates.ToListAsync();
            return list.Select(MapToDomain);
        }

        public async Task<IEnumerable<DepositCertificateReadOnlyView>> GetDetailAsync()
        {
            var certificateList = from c in _context.DepositCertificates
                                  join b in _context.Banks on c.BankId equals b.BankId
                                  join cr in _context.Currencies on c.CurrencyId equals cr.CurrencyId
                                  select new DepositCertificateReadOnlyView
                                  {
                                      CertificateId = c.CertificateId,
                                      Bank = b.Description,
                                      Currency = cr.Description,
                                      Description = c.Description,
                                      Amount = c.Amount,
                                      Interest_amount = c.InterestAmount,
                                      Expiration_date = c.ExpirationDate,
                                      IsActive = c.IsActive                                 
                                  };

            var result = await certificateList.ToListAsync();

            return result;
        }

        public async Task<DepositCertificateEntity> GetByIdAsync(int id)
        {
            var e = await _context.DepositCertificates.FindAsync(id);
            return MapToDomain(e);
        }

        public async Task<DepositCertificateEntity> AddAsync(DepositCertificateEntity Certificate)
        {
            var entity = new DepositCertificate();
            MapToEntity(Certificate, entity);
            _context.DepositCertificates.Add(entity);
            await _context.SaveChangesAsync();

            return MapToDomain(entity);
        }

        public async Task UpdateAsync(DepositCertificateEntity Certificate)
        {
            var entity = await _context.DepositCertificates.FindAsync(Certificate.CertificateId);
            if (entity == null) throw new
            KeyNotFoundException($"Certificate with id {Certificate.CertificateId} not found.");
            MapToEntity(Certificate, entity);
            _context.DepositCertificates.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDepositCertificateAsync(int certificateId)
        {
            var certificate = await _context.DepositCertificates.FindAsync(certificateId);
            if (certificate == null) return;

            _context.DepositCertificates.Remove(certificate);
            await _context.SaveChangesAsync();
        }
    }
}
