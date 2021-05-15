using FluentValidation;

namespace AngloAmerican.Account.Api.Validators
{
    public class AccountRequestValidator : AbstractValidator<AccountRequest>
    {
        public AccountRequestValidator()
        {
            RuleFor(model => model.FirstName)
                .NotNull()
                .NotEmpty();

            RuleFor(model => model.LastName)
                .NotNull()
                .NotEmpty();

            RuleFor(model => model.Balance)
                .GreaterThanOrEqualTo(0);
        }
    }
}