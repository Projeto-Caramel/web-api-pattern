using Caramel.Pattern.Services.Domain.Entities;
using FluentValidation;

namespace Caramel.Pattern.Services.Domain.Validations
{
    public class PetValidator : AbstractValidator<Pet>
    {
        public PetValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("O ID deve ser maior do que 0.");
            RuleFor(x => x.PartnerId).GreaterThan(0).WithMessage("O Partner ID deve ser maior do que 0.");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("O campo Nome é Obrigatório.");
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("O campo Descrição é Obrigatório.");
            RuleFor(x => x.Age).GreaterThanOrEqualTo(0).WithMessage("A Idade deve ser maior do que 0.");
            RuleFor(x => x.Status).IsInEnum().WithMessage("O Status deve ser um valor entre 0 e 4.");
        }
    }
}
