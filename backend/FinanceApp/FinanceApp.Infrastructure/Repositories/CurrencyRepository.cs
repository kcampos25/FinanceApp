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
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly FinanceDbContext _context;
        public CurrencyRepository(FinanceDbContext context)
        {
            _context = context;
        }
        private CurrencyEntity MapToDomain(Currency e)
        {
            if (e == null) return null;
            return new CurrencyEntity
            {
                CurrencyId = e.CurrencyId,
                Description = e.Description,
                CreatedBy = e.CreatedBy,
                CreatedAt = e.CreatedAt,
                UpdatedBy = e.UpdatedBy,
                UpdatedAt = e.UpdatedAt
            };
        }
        private void MapToEntity(CurrencyEntity domain, Currency data)
        {
            data.Description = domain.Description;
            data.CreatedBy = domain.CreatedBy;
            data.CreatedAt = domain.CreatedAt;
            data.UpdatedBy = domain.UpdatedBy;
            data.UpdatedAt = domain.UpdatedAt;
        }
        public async Task<CurrencyEntity> AddAsync(CurrencyEntity currency)
        {
            var entity = new Currency();
            MapToEntity(currency, entity);
            _context.Currencies.Add(entity);
            await _context.SaveChangesAsync();

            return MapToDomain(entity);
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Currencies.FindAsync(id);
            if (entity == null) return;
            _context.Currencies.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<CurrencyEntity>> GetAllAsync()
        {
            var list = await _context.Currencies.ToListAsync();
            return list.Select(MapToDomain);
        }
        public async Task<CurrencyEntity> GetByIdAsync(int id)
        {
            var e = await _context.Currencies.FindAsync(id);
            return MapToDomain(e);
        }
        public async Task UpdateAsync(CurrencyEntity currency)
        {
            var entity = await _context.Currencies.FindAsync(currency.CurrencyId);
            if (entity == null) throw new
            KeyNotFoundException($"Currency with id {currency.CurrencyId} not found.");
            MapToEntity(currency, entity);
            _context.Currencies.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ListItemEntity>> GetCurrencyLookupAsync()
        {
            var currencylist = await _context.Currencies.ToListAsync();
            return (currencylist == null) ? null : currencylist.Select(c => new ListItemEntity
            {
                Code = c.CurrencyId,
                Description = c.Description
            });
        }
    }
}
