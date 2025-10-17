using AutoMapper;
using FinanceApp.Application.DTOs;
using FinanceApp.Application.Services;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Interfaces.Repositories;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FinanceApp.Application.Tests
{
    public class CertificationService_Tests
    {
        private readonly Mock<IDepositCertificateRepository> _certRepoMock;
        private readonly Mock<IBankRepository> _bankRepoMock;
        private readonly Mock<ICurrencyRepository> _currencyRepoMock;
        private readonly IMapper _mapper;
        private readonly DepositCertificateService _sut; // system under test

        public CertificationService_Tests()
        {
            _certRepoMock = new Mock<IDepositCertificateRepository>();
            _bankRepoMock = new Mock<IBankRepository>();
            _currencyRepoMock = new Mock<ICurrencyRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DepositCertificateEntity, DepositCertificateDTO>();
                cfg.CreateMap<CreateDepositCertificateDTO, DepositCertificateEntity>();
                cfg.CreateMap<UpdateDepositCertificateDTO, DepositCertificateEntity>();
                cfg.CreateMap<DepositCertificateEntity, DepositCertificateReadOnlyViewDTO>();
            });

            _mapper = config.CreateMapper();

            _sut = new DepositCertificateService(
                    _certRepoMock.Object,
                    _bankRepoMock.Object,
                    _currencyRepoMock.Object,
                    _mapper
            );   

        }


        [Fact]
        public async Task GetByIdAsync_ShouldReturnDTO_WhenCertificationExists()
        {
            // Arrange
            var entity = new DepositCertificateEntity { CertificateId = 1, Owner_name = "Juan" };
            _certRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);

            // Act
            var result = await _sut.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result.CertificateId.Should().Be(1);
            result.Owner_name.Should().Be("Juan");
        }

        // 2️⃣ Caso no encontrado: ID válido pero no existe
        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenCertificationDoesNotExist()
        {
            // Arrange
            _certRepoMock.Setup(r => r.GetByIdAsync(2)).ReturnsAsync((DepositCertificateEntity)null);

            // Act
            var result = await _sut.GetByIdAsync(2);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowKeyNotFoundException_WhenBankDoesNotExist()
        {
            // Arrange
            var dto = new CreateDepositCertificateDTO
            {
                BankId = 99,
                CurrencyId = 1,
                Owner_name = "Juan",
                Amount = 100,
                Start_date= DateTime.Now,
                Expiration_date =DateTime.Now
            };

            _bankRepoMock.Setup(b => b.GetByIdAsync(99)).ReturnsAsync((BankEntity)null);
            _currencyRepoMock.Setup(c => c.GetByIdAsync(1)).ReturnsAsync(new CurrencyEntity { CurrencyId = 1 });

            // Act
            Func<Task> act = async () => await _sut.CreateAsync(dto);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage("Bank with id 99 not found.");
        }
    }
}
