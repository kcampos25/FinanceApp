using FinanceApp.Domain.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FinanceApp.Application.Tests
{
    public class CardDomain_Tests
    {
        private readonly CardEntity _sut;

        public CardDomain_Tests()
        {
            _sut = new CardEntity();
        }

        [Fact]
        public void ValidateRules_ShouldReturnExeption_when_CutOffDayislessthanzero()
        {
            _sut.CardTypeId = 1;
            _sut.BankId = 1;
            _sut.CurrencyId = 1;
            _sut.Comment = "test";
            _sut.OwnerName = "test";
            _sut.IssueDate = DateTime.Now;
            _sut.ExpirationDate = DateTime.Now;
            _sut.CutOffDay = 0;

            Action act = () => _sut.ValidateRules();

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("cut-off day must be greater than zero");

        }
    }
}
