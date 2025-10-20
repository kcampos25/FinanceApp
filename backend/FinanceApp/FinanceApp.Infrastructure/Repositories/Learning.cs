using FinanceApp.Domain.Entities;
using FinanceApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace FinanceApp.Infrastructure.Repositories
{
    // class to practice
    public class Learning
    {
        private readonly FinanceDbContext _context;
        public Learning(FinanceDbContext context)
        {
            _context = context;
        }

        //private BankEntity MapToEntity(Bank bank)
        //{
        //    if (bank == null) return null;

        //    return new BankEntity
        //    {
        //        BankId = bank.BankId,
        //        Description = bank.Description
        //    };
        //}

        // Practice 1
        //********************************************************************************************


        //1.1 Consulta por id
        public async Task<BankEntity> getById(int id)
        {
            var bank = await _context.Banks.FindAsync(id);

            //return (bank == null) ? null : new BankEntity
            //{
            //    BankId = bank.BankId,
            //    Description = bank.Description           
            //};

            return MapToEntity(bank);
        }

        // 1.2 consulta por id

        public async Task<BankEntity> GetBankById(int id)
        {
            var bank = await _context.Banks.SingleOrDefaultAsync(c => c.BankId == id);

            return MapToEntity(bank);
        }

        // 1.3 Consulta por id
        public async Task<BankEntity> GetById_BankTest(int id)
        {
            var bank = await _context.Banks
                             .Select(c => new BankEntity()
                             {
                                 BankId = c.BankId
                             }).SingleOrDefaultAsync(c => c.BankId == id);

            return bank;
        }


        // Practice 2
        //********************************************************************************************

        // 2.1 Consulta general

        public async Task<IEnumerable<BankEntity>> getAllBanks()
        {
            var banks = await _context.Banks.ToListAsync();

            //return banks.Select( c => new BankEntity(){ 

            //    BankId =c.BankId
            //});

            return banks.Select(MapToEntity);
        }

        // 2.2 Consulta general 2
        public async Task<IEnumerable<BankEntity>> GetAllBank2()
        {
            var banks = await _context.Banks
                               .Select(c => new BankEntity()
                               {

                                   BankId = c.BankId
                               }).ToListAsync();

            return banks;
        }


        // 2.3 Consulta general 3
        public async Task<IEnumerable<BankEntity>> GetAllBank3(int id, string description)
        {
            var banks = await _context.Banks
                         .Where(c => c.BankId == id && c.Description == description)
                         .Select(c => new BankEntity()
                         {

                             BankId = c.BankId
                         })
                         .ToListAsync();

            return banks;
        }


        // 2.3 Consulta general 3
        public async Task<IEnumerable<BankEntity>> GetInfo(int id)
        {
            var banks = await (from bank in _context.Banks
                               join card in _context.Cards on bank.BankId equals card.BankId
                               where bank.BankId == id && card.Comment == "test"
                               orderby bank.BankId
                               select new BankEntity
                               {
                                   BankId = bank.BankId,
                                   Description = card.Comment
                               }).ToListAsync();

            return banks;
        }

        // 2.4 Consulta general 4
        public async Task<IEnumerable<BankEntity>> GetInfoLetf(int id)
        {
            var banks = await (from c in _context.Cards
                               join b in _context.Banks on c.BankId equals b.BankId into BankDetails
                               from b in BankDetails.DefaultIfEmpty()
                               where c.BankId == id
                               select new BankEntity
                               {
                                   BankId = c.BankId
                               }
                               ).ToListAsync();

            return banks;
        }


        private Bank MaptoBankModel(BankEntity bankEntity)
        {
            return new Bank()
            {
                Description = bankEntity.Description,
                CreatedBy = bankEntity.CreatedBy,
                CreatedAt = DateTime.Now
            };
        }

        private BankEntity MapToEntity(Bank bank)
        {
            if (bank == null) return null;

            return new BankEntity
            {
                BankId = bank.BankId,
                Description = bank.Description
            };
        }

        public async Task<BankEntity> Insert(BankEntity bankEntity)
        {

            var bankModel = MaptoBankModel(bankEntity);

            _context.Banks.Add(bankModel);
            await _context.SaveChangesAsync();

            return MapToEntity(bankModel);

        }

        public async Task update(int id, BankEntity bankEntity)
        {
            var bank = await _context.Banks.FindAsync(id);
            if (bank == null) throw new KeyNotFoundException($" key not found {id}");

            bank.Description = bankEntity.Description;
            bank.UpdatedBy = "kcampos";
            bank.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
        }



    }
}
