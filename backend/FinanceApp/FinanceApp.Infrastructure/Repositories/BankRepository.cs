using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Interfaces.Repositories;
using FinanceApp.Domain.ListItem;
using FinanceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Repositories
{
    //bank repository
    public class BankRepository : IBankRepository
    {

        private readonly FinanceDbContext _context;
        public BankRepository(FinanceDbContext context)
        {
            _context = context;
        }
        private BankEntity MapToDomain(Bank e)
        {
            if (e == null) return null;
            return new BankEntity
            {
                BankId = e.BankId,
                Description = e.Description,
                CreatedBy = e.CreatedBy,
                CreatedAt = e.CreatedAt,
                UpdatedBy = e.UpdatedBy,
                UpdatedAt = e.UpdatedAt
            };
        }
        private void MapToEntity(BankEntity domain, Bank data)
        {
            data.Description = domain.Description;
            data.CreatedBy = domain.CreatedBy;
            data.CreatedAt = domain.CreatedAt;
            data.UpdatedBy = domain.UpdatedBy;
            data.UpdatedAt = domain.UpdatedAt;
        }
        public async Task<BankEntity> AddAsync(BankEntity bank)
        {
            var entity = new Bank();
            MapToEntity(bank, entity);
            _context.Banks.Add(entity);
            await _context.SaveChangesAsync();

            return MapToDomain(entity);
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Banks.FindAsync(id);
            if (entity == null) return;
            _context.Banks.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<BankEntity>> GetAllAsync()
        {
            var list = await _context.Banks.ToListAsync();
            return list.Select(MapToDomain);
        }
        public async Task<BankEntity> GetByIdAsync(int id)
        {
            var e = await _context.Banks.FindAsync(id);
            return MapToDomain(e);
        }

        public async Task<IEnumerable<ListItemEntity>> GetBankLookupAsync()
        {
            var banklist = await _context.Banks.ToListAsync();
            return (banklist == null) ? null : banklist.Select(c => new ListItemEntity
            {
                Code = c.BankId,
                Description = c.Description
            });
        }
        public async Task UpdateAsync(BankEntity bank)
        {
            var entity = await _context.Banks.FindAsync(bank.BankId);
            if (entity == null) throw new
            KeyNotFoundException($"Bank with id {bank.BankId} not found.");
            MapToEntity(bank, entity);
            _context.Banks.Update(entity);
            await _context.SaveChangesAsync();
        }
    }

}
