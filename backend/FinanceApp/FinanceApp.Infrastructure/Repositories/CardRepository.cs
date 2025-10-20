using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Interfaces;
using FinanceApp.Domain.ListItem;
using FinanceApp.Domain.Queries;
using FinanceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Repositories
{
    //card repository
    public class CardRepository : ICardRepository
    {

        private readonly FinanceDbContext _context;

        public CardRepository(FinanceDbContext context)
        {
            _context = context;
        }

        private CardEntity MapToDomain(Card model)
        {
            if (model == null) return null;

            return new CardEntity
            {
                CardId = model.CardId,
                BankId = model.BankId,
                CurrencyId = model.CurrencyId,
                CardTypeId = model.CardTypeId,
                OwnerName = model.OwnerName,
                Comment = model.Comment,
                IssueDate = model.IssueDate,
                ExpirationDate = model.ExpirationDate,
                CutOffDay = model.CutOffDay,
                PaymentDay = model.PaymentDay,
                CreatedBy = model.CreatedBy,
                CreatedAt = model.CreatedAt,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };

        }

        private void MapToModelEF(CardEntity domain, Card model)
        {
            model.BankId = domain.BankId;
            model.CurrencyId = domain.CurrencyId;
            model.CardTypeId = domain.CardTypeId;
            model.OwnerName = domain.OwnerName;
            model.Comment = domain.Comment;
            model.IssueDate = domain.IssueDate;
            model.ExpirationDate = domain.ExpirationDate;
            model.CutOffDay = domain.CutOffDay;
            model.PaymentDay = domain.PaymentDay;

            if (domain.CardId > 0)
            {
                model.UpdatedBy = domain.UpdatedBy;
                model.UpdatedAt = domain.UpdatedAt;
            }
            else
            {
                model.CreatedBy = domain.CreatedBy;
                model.CreatedAt = domain.CreatedAt;
            }
        }

        public async Task<IEnumerable<CardEntity>> GetAllAsync()
        {
            var cardlist = await _context.Cards.ToListAsync();
            return cardlist.Select(MapToDomain);
        }

        public async Task<IEnumerable<CardReadOnlyView>> GetDetailAsync()
        {

            //return await (from cards in _context.Cards
            //              join banks in _context.Banks on cards.BankId equals banks.BankId
            //              join currencies in _context.Currencies on cards.CurrencyId equals currencies.CurrencyId
            //              join cardTypes in _context.CardTypes on cards.CardTypeId equals cardTypes.CardTypeId
            //              select new CardReadOnlyView
            //              {
            //                  CardId = cards.CardId,
            //                  Bank = banks.Description,
            //                  Currency = currencies.Description,
            //                  CardType = cardTypes.CardTypeCode,
            //                  OwnerName = cards.OwnerName,
            //                  Comment = cards.Comment,
            //                  IssueDate = cards.IssueDate,
            //                  ExpirationDate = cards.ExpirationDate,
            //                  CutOffDay = (cards.CutOffDay > 0) ? "Day:" + cards.CutOffDay.ToString() + "/ Monthly." : "N/A",
            //                  PaymentDay = (cards.PaymentDay > 0) ? "Day:" + cards.PaymentDay.ToString() + "/ Monthly." : "N/A"

            //              }).ToListAsync();

            return await _context.CardDetail.FromSqlInterpolated($"EXEC sp_GetDetail_Banks").ToListAsync();

        }

        public async Task<CardEntity> GetByIdAsync(int id)
        {
            if (id <= 0) return null;
            var card = await _context.Cards.FindAsync(id);

            return MapToDomain(card);
        }

        public async Task<CardEntity> AddAsync(CardEntity card)
        {
            var cardModel = new Card();
            MapToModelEF(card, cardModel);
            _context.Cards.Add(cardModel);
            await _context.SaveChangesAsync();

            return MapToDomain(cardModel);
        }

        public async Task UpdateAsync(CardEntity cardEntity)
        {
            var card = await _context.Cards.FindAsync(cardEntity.CardId);

            if (card == null) throw new KeyNotFoundException($"Card with id {cardEntity.CardId} not found.");

            MapToModelEF(cardEntity, card);
            _context.Cards.Update(card);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Cards.FindAsync(id);
            if (entity == null) return;
            _context.Cards.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ListItemEntity>> GetCardTypeLookupAsync()
        {
            var cardTypelist = await _context.CardTypes.ToListAsync();
            return (cardTypelist == null) ? null : cardTypelist.Select(c => new ListItemEntity
            {
                Code = c.CardTypeId,
                Description = c.Description
            });
        }

    }
}
