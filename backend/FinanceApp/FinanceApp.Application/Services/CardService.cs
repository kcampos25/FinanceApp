using AutoMapper;
using FinanceApp.Application.DTOs;
using FinanceApp.Application.Interfaces;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Interfaces;
using FinanceApp.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceApp.Application.Services
{
    //card service
    public class CardService: ICardService
    {

        private readonly ICardRepository _cardRepository;
        private readonly IBankRepository _bankRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper _mapper;

        public CardService(ICardRepository cardRepository, IMapper mapper, IBankRepository bankRepository, ICurrencyRepository currencyRepository)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
            _bankRepository = bankRepository;
            _currencyRepository = currencyRepository;
        }
    
        public async Task<CardDTO> GetByIdAsync(int id)
        {
            var card = await _cardRepository.GetByIdAsync(id);
            if (card == null) return null;
            return _mapper.Map<CardDTO>(card);
        }

        public async Task<IEnumerable<CardDTO>> GetAllAsync()
        {
            var cards = await _cardRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CardDTO>>(cards);
        }

        public async Task<CardDTO> CreateAsync(CreateCardDTO dto)
        {
            var card = new CardEntity
            {
                BankId = dto.BankId,
                CurrencyId = dto.CurrencyId,
                CardTypeId = dto.CardTypeId,
                OwnerName = dto.OwnerName,
                Comment = dto.Comment,
                IssueDate = dto.IssueDate,
                ExpirationDate = dto.ExpirationDate,
                CutOffDay = dto.CutOffDay,
                PaymentDay = dto.PaymentDay,
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.Now
            };
            card.ValidateRules(); 

            await ValidateApplicationRules(card);

            var created = await _cardRepository.AddAsync(card);
            return _mapper.Map<CardDTO>(created);
        }
        public async Task UpdateAsync(int id, UpdateCardDTO dto)
        {
            var cardEntity = await _cardRepository.GetByIdAsync(id);

            if (cardEntity == null)
            {
                throw new KeyNotFoundException($"Card with id {id} not found.");
            }

            _mapper.Map(dto, cardEntity);
            cardEntity.CardId = id;
            cardEntity.UpdatedAt = DateTime.Now;

            cardEntity.ValidateRules(); // reglas criticas de dominio
            await ValidateApplicationRules(cardEntity);
            await _cardRepository.UpdateAsync(cardEntity);
        }

        public async Task<IEnumerable<CardReadOnlyViewDTO>> GetDetailAsync()
        {
            var cardDetail = await _cardRepository.GetDetailAsync();
            return _mapper.Map<IEnumerable<CardReadOnlyViewDTO>>(cardDetail);
        }

        public async Task DeleteAsync(int id)
        {
            var card = await _cardRepository.GetByIdAsync(id);
            if (card == null) throw new KeyNotFoundException($"Card with id {id} not found");
            await _cardRepository.DeleteAsync(id);
        }

        private async Task ValidateApplicationRules(CardEntity card)
        {
            var bankExists = await _bankRepository.GetByIdAsync(card.BankId);
            if (bankExists == null)
                throw new KeyNotFoundException($"Bank with id {card.BankId} not found.");

            var currencyExists = await _currencyRepository.GetByIdAsync(card.CurrencyId);
            if (currencyExists == null)
                throw new KeyNotFoundException($"Currency with id {card.CurrencyId} not found.");
        }

        public async Task<IEnumerable<ListItemDTO>> GetCardTypeLookupAsync()
        {
            var cardTypes = await _cardRepository.GetCardTypeLookupAsync();
            return _mapper.Map<IEnumerable<ListItemDTO>>(cardTypes);
        }

    }
}
