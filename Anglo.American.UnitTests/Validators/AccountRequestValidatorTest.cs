using AngloAmerican.Account.Api;
using AngloAmerican.Account.Api.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace Anglo.American.UnitTests.Validators
{
    public class AccountRequestValidatorTest
    {
        private readonly AccountRequestValidator _accountRequestValidator;

        public AccountRequestValidatorTest()
        {
            _accountRequestValidator = new AccountRequestValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        public void Shoud_Have_Error_When_FirstName_Is_Null_And_FirstName_Is_Empty(string firstName)
        {
            var model = new AccountRequest
            {
                FirstName = firstName
            };

            var result = _accountRequestValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.FirstName);
        }

        [Theory]
        [InlineData("Robert")]
        [InlineData("Phil")]
        public void Shoud_Not_Have_Error_When_FirstName_Is_Valid(string firstName)
        {
            var model = new AccountRequest
            {
                FirstName = firstName
            };

            var result = _accountRequestValidator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.FirstName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        public void Shoud_Have_Error_When_LastName_Is_Null_And_LastName_Is_Empty(string lastName)
        {
            var model = new AccountRequest
            {
                FirstName = lastName
            };

            var result = _accountRequestValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.LastName);
        }

        [Theory]
        [InlineData("Griffits")]
        [InlineData("Smith")]
        public void Shoud_Not_Have_Error_When_LastName_Is_Valid(string lastName)
        {
            var model = new AccountRequest
            {
                LastName = lastName
            };

            var result = _accountRequestValidator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.LastName);
        }
    }
}