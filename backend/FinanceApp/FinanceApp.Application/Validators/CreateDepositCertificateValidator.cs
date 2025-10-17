using FinanceApp.Application.DTOs;
using FluentValidation;

namespace FinanceApp.Application.Validators
{
    public class CreateDepositCertificateValidator : AbstractValidator<CreateDepositCertificateDTO>
    {
        public CreateDepositCertificateValidator()
        {
            RuleFor(x => x.BankId).NotEmpty();
            RuleFor(x => x.CurrencyId).NotEmpty();
            RuleFor(x => x.Owner_name).MaximumLength(20);
            RuleFor(x => x.Description).MaximumLength(20);
            RuleFor(x => x.Comment).MaximumLength(2000);
            RuleFor(x => x.Amount).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(x => x.Interest_amount).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(x => x.Start_date).NotEmpty();
            RuleFor(x => x.Expiration_date).NotEmpty();
            RuleFor(x => x.CreatedBy).NotEmpty().MaximumLength(60);
        }
    }

    public class UpdateCertificationValidator : AbstractValidator<UpdateDepositCertificateDTO>
    {
        public UpdateCertificationValidator()
        {
            RuleFor(x => x.BankId).NotEmpty();
            RuleFor(x => x.CurrencyId).NotEmpty();
            RuleFor(x => x.Owner_name).MaximumLength(20);
            RuleFor(x => x.Description).MaximumLength(20);
            RuleFor(x => x.Comment).MaximumLength(2000);
            RuleFor(x => x.Amount).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(x => x.Interest_amount).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(x => x.Start_date).NotEmpty();
            RuleFor(x => x.Expiration_date).NotEmpty();
            RuleFor(x => x.UpdatedBy).NotEmpty().MaximumLength(60);
        }
    }
}
