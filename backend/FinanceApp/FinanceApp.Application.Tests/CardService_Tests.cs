using AutoMapper;
using FinanceApp.Application.DTOs;
using FinanceApp.Application.Services;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Interfaces;
using FinanceApp.Domain.Interfaces.Repositories;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FinanceApp.Application.Tests
{
    public class CardService_Tests
    {
        private readonly CardService _sut;
        private readonly Mock<ICardRepository> _cardRepositoryMock;
        private readonly Mock<IBankRepository> _bankRepositoryMock;
        private readonly Mock<ICurrencyRepository> _currencyRepositoryMock;
        private readonly IMapper _mapper;
        public CardService_Tests()
        {
            _cardRepositoryMock = new Mock<ICardRepository>();
            _bankRepositoryMock = new Mock<IBankRepository>();
            _currencyRepositoryMock = new Mock<ICurrencyRepository>();


            var config = new MapperConfiguration(cfg =>
             {
                 cfg.CreateMap<CardEntity, CardDTO>();
                 cfg.CreateMap<UpdateCardDTO, CardEntity>();
             });

            _mapper = config.CreateMapper();

            _sut = new CardService(
                _cardRepositoryMock.Object,
                _mapper,
                _bankRepositoryMock.Object,
                _currencyRepositoryMock.Object
                );
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnDTO_WhenMethodExist()
        {
            //arrange
            var idCard = 1;

            //act
            var result = _sut.GetByIdAsync(idCard);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<CardDTO>>();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnDTO_WhenCardExist()
        {
            //Arrange

            var entity = new CardEntity { CardId=1,BankId=1,CurrencyId=1,OwnerName="kenneth",Comment="test" };
            _cardRepositoryMock.Setup(card => card.GetByIdAsync(1)).ReturnsAsync(entity);

            // Act
            var result = await _sut.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CardDTO>();
            result.CardId.Should().Be(1);
            result.OwnerName.Should().Be("kenneth");
        }


        [Fact]
        public async Task GetByIdAsync_ShouldReturn_null_WhenCardnotExist()
        {
            //Arrange
            _cardRepositoryMock.Setup(card => card.GetByIdAsync(1)).ReturnsAsync((CardEntity)null);

            // Act
            var result = await _sut.GetByIdAsync(1);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnDTO_WhenMethodExist()
        {
            // Arrange
            var cardDto = new CreateCardDTO
            {
                BankId = 1,
                CurrencyId = 1,
                OwnerName = "kenneth",
                CardTypeId = 1,
                Comment = "test",
                IssueDate = DateTime.Now,
                ExpirationDate = DateTime.Now
            };

            _bankRepositoryMock.Setup(b => b.GetByIdAsync(1))
                .ReturnsAsync(new BankEntity { BankId = 1 });

            _currencyRepositoryMock.Setup(c => c.GetByIdAsync(1))
                .ReturnsAsync(new CurrencyEntity { CurrencyId = 1 });

            _cardRepositoryMock.Setup(c => c.AddAsync(It.IsAny<CardEntity>()))
                .ReturnsAsync((CardEntity c) => c); // opcional para devolver el mismo

            // Act
            var result = await _sut.CreateAsync(cardDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CardDTO>();
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnDTO_WhenBank_CurrencyExist()
        {
            //arrange
            var cardDto = new CreateCardDTO { BankId = 1, CurrencyId = 1, OwnerName = "kenneth", CardTypeId = 1,Comment="test", IssueDate=DateTime.Now, ExpirationDate = DateTime.Now };
            var cardEntity = new CardEntity { BankId = 1, CurrencyId = 1, OwnerName = "kenneth", CardTypeId=1, Comment = "test", IssueDate = DateTime.Now, ExpirationDate = DateTime.Now };
            _cardRepositoryMock.Setup(card => card.AddAsync(It.Is<CardEntity>(c => c.BankId==1 && c.CurrencyId==1 && c.OwnerName== "kenneth" && c.Comment == "test" && c.CardTypeId ==1) )).ReturnsAsync(cardEntity);

            var bank = new BankEntity { BankId = 1, Description = "BAC" };
            _bankRepositoryMock.Setup(bank => bank.GetByIdAsync(1)).ReturnsAsync(bank);

            var currency = new CurrencyEntity { CurrencyId = 1, Description = "DOL" };
            _currencyRepositoryMock.Setup(currency => currency.GetByIdAsync(1)).ReturnsAsync(currency);

            //act
            var result = await _sut.CreateAsync(cardDto);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CardDTO>();
            result.BankId.Should().Be(1);
            result.OwnerName.Should().Be("kenneth");
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnDTO_WhenMethodExist()
        {
            //arrange

            var cardDto = new UpdateCardDTO { BankId = 1, CurrencyId = 1, OwnerName = "kenneth", CardTypeId=1,Comment="test",IssueDate=DateTime.Now,ExpirationDate=DateTime.Now };
            var cardId = 1;

            var cardEntity = new CardEntity { CardId = 1, BankId = 1, CurrencyId = 1 , CardTypeId = 1, Comment = "test", IssueDate = DateTime.Now, ExpirationDate = DateTime.Now };
            _cardRepositoryMock.Setup(c => c.GetByIdAsync(1)).ReturnsAsync(cardEntity);
           
            _bankRepositoryMock.Setup(b => b.GetByIdAsync(1))
                   .ReturnsAsync(new BankEntity { BankId = 1 });

            _currencyRepositoryMock.Setup(c => c.GetByIdAsync(1))
                .ReturnsAsync(new CurrencyEntity { CurrencyId = 1 });
            //act

            await _sut.UpdateAsync(cardId, cardDto);

            //assert

            _cardRepositoryMock.Verify(r => r.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnExeption_WhenRecordnotExist()
        {
            //arrange

            var cardDto = new UpdateCardDTO { BankId = 1, CurrencyId = 1, OwnerName = "kenneth" };
            var cardId = 1;

            var cardEntity = new CardEntity { CardId = 1, BankId = 1, CurrencyId = 1 };
            _cardRepositoryMock.Setup(c => c.GetByIdAsync(1)).ReturnsAsync((CardEntity)null);

            //act

            //await _sut.UpdateAsync(cardId, cardDto);

            Func<Task> act = async () => await _sut.UpdateAsync(cardId, cardDto);

            //assert
            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Card with id 1 not found.");
            _cardRepositoryMock.Verify(r => r.GetByIdAsync(1), Times.Once);
            
           
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdatePropertiesCorrectly()
        {
            // Arrange
            var card = new CardEntity {BankId=1,CurrencyId=1,CardTypeId=1, OwnerName = "OldName", Comment = "Old", IssueDate = DateTime.Now, ExpirationDate = DateTime.Now };
            _cardRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(card);

            var dto = new UpdateCardDTO { BankId = 1, CurrencyId = 1, CardTypeId = 1, OwnerName = "NewName", Comment = "NewComment" , IssueDate = DateTime.Now, ExpirationDate = DateTime.Now };

            _bankRepositoryMock.Setup(b => b.GetByIdAsync(1))
        .ReturnsAsync(new BankEntity { BankId = 1 });

            _currencyRepositoryMock.Setup(c => c.GetByIdAsync(1))
                .ReturnsAsync(new CurrencyEntity { CurrencyId = 1 });

            // Act
            await _sut.UpdateAsync(1, dto);

            // Assert
            card.OwnerName.Should().Be("NewName");
            card.Comment.Should().Be("NewComment");
            card.CardId.Should().Be(1);
            card.UpdatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnExeption_WhenBanknotExist()
        {
            //arrange

            var card = new CardEntity { BankId=1, CurrencyId=1, CardTypeId=1, OwnerName = "OldName", Comment = "Old", IssueDate = DateTime.Now, ExpirationDate = DateTime.Now };
            _cardRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(card);

            _bankRepositoryMock.Setup(bank => bank.GetByIdAsync(1)).ReturnsAsync((BankEntity)null);

            var dto = new UpdateCardDTO { BankId = 1, CurrencyId = 1, CardTypeId=1, OwnerName = "NewName", Comment = "NewComment", IssueDate = DateTime.Now, ExpirationDate = DateTime.Now };


            Func<Task> act = async () => await _sut.UpdateAsync(1, dto);

            //assert
            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Bank with id 1 not found.");
        }

    }

}