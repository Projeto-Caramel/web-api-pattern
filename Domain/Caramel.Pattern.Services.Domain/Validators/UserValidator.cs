using Caramel.Pattern.Services.Domain.Entities;
using FluentValidation;

namespace Caramel.Pattern.Services.Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator() {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("O campo Nome é Obrigatório.");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("O campo Sobrenome é Obrigatório.");
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("O campo Email é Obrigatório.");
            RuleFor(x => x.Phone).NotNull().NotEmpty().WithMessage("O campo Telefone é Obrigatório.");
            RuleFor(x => x.Cpf).NotNull().NotEmpty().WithMessage("O campo CPF é Obrigatório.");
        }
    }
}
