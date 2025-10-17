using FinanceApp.Application.DTOs;
using FluentValidation;

namespace FinanceApp.Application.Validators
{
    public class CreateBankValidator : AbstractValidator<CreateBankDTO>
    {
        public CreateBankValidator()
        {
            RuleFor(x => x.Description).NotEmpty().MaximumLength(100);
            RuleFor(x => x.CreatedBy).NotEmpty().MaximumLength(60);
        }
    }

    public class UpdateBankValidator : AbstractValidator<UpdateBankDTO>
    {
        public UpdateBankValidator()
        {
            RuleFor(x => x.Description).NotEmpty().MaximumLength(100);
            RuleFor(x => x.UpdatedBy).NotEmpty().MaximumLength(60);
        }
    }
}
