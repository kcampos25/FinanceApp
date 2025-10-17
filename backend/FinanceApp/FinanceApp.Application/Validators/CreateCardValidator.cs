using FinanceApp.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Application.Validators
{
    public class CreateCardValidator : AbstractValidator<CreateCardDTO>
    {
        public CreateCardValidator()
        {
            RuleFor(x => x.BankId).NotEmpty();
            RuleFor(x => x.CurrencyId).NotEmpty();
            RuleFor(x => x.CardTypeId).NotEmpty();
            RuleFor(x => x.OwnerName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Comment).NotEmpty().MaximumLength(250);
            RuleFor(x => x.IssueDate).NotEmpty();
            RuleFor(x => x.ExpirationDate).NotEmpty();
            RuleFor(x => x.CreatedBy).NotEmpty().MaximumLength(60);
        }
    }

    public class UpdateCardValidator : AbstractValidator<UpdateCardDTO>
    {
        public UpdateCardValidator()
        {
            RuleFor(x => x.BankId).NotEmpty();
            RuleFor(x => x.CurrencyId).NotEmpty();
            RuleFor(x => x.CardTypeId).NotEmpty();
            RuleFor(x => x.OwnerName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Comment).NotEmpty().MaximumLength(250);
            RuleFor(x => x.IssueDate).NotEmpty();
            RuleFor(x => x.ExpirationDate).NotEmpty();
            RuleFor(x => x.UpdatedBy).NotEmpty().MaximumLength(60);
        }
    }
}
