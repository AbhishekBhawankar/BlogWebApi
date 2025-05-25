using BlogWebApi.DTO;
using FluentValidation;

namespace BlogWebApi.Validations
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("FullName is required..");


            RuleFor(x => x.MobileNo)
                .NotEmpty().WithMessage("Mobile Number is Required.")
                .MinimumLength(10).WithMessage("Mobile Number Must be in 10 digit");


            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("A valid email is required");


            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");

        }
    }
}
