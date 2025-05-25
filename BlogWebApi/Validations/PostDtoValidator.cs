using BlogWebApi.DTO;
using FluentValidation;

namespace BlogWebApi.Validations
{
    public class PostDtoValidator : AbstractValidator<PostDTO>
    {
        public PostDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.");


            RuleFor(x => x.File)
                .NotEmpty().WithMessage("Selected File is required.");
        }

    }
}
