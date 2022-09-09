using FluentValidation;
using System.Linq;
using web_api_net5.Entities;
using web_api_net5.Models;

namespace web_api_net5.Models.Validator
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(RestaurantDbContext dbContext)
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.Password).MinimumLength(6);

            RuleFor(u => u.Password).Equal(e => e.Password);

            RuleFor(u => u.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);
                    if (emailInUse)
                        context.AddFailure("Email", "Email is already in use");
                });

        }

    }
}
