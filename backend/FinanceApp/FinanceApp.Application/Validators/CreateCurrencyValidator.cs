using FinanceApp.Application.DTOs;
using FluentValidation;


namespace FinanceApp.Application.Validators
{
    public class CreateCurrencyValidator: AbstractValidator<CreateCurrencyDTO>
    {
        public CreateCurrencyValidator()
        {
            RuleFor(x => x.Description).NotEmpty().MaximumLength(20);
            RuleFor(x => x.CreatedBy).NotEmpty().MaximumLength(60);
        }
    }

    public class UpdateCurrencyValidator : AbstractValidator<UpdateCurrencyDTO>
    {
        public UpdateCurrencyValidator()
        {
            RuleFor(x => x.Description).NotEmpty().MaximumLength(20);
            RuleFor(x => x.UpdatedBy).NotEmpty().MaximumLength(60);
        }
    }
}
