using FluentValidation;
using __NAME__.Messages.Examples;

namespace __NAME__.App.Api
{
    public class NewExampleValidator : AbstractValidator<NewExampleModel>
    {
        public NewExampleValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }

    public class CloseExampleValidator : AbstractValidator<CloseExampleModel>
    {
        public CloseExampleValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        }
    }
}