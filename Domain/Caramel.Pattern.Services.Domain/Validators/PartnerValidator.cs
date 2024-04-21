using Caramel.Pattern.Services.Domain.Entities;
using FluentValidation;

namespace Caramel.Pattern.Services.Domain.Validators
{
    public class PartnerValidator : AbstractValidator<Partner>
    {
        public PartnerValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("O ID deve ser maior do que 0.");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("O campo Nome é Obrigatório.");
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("O campo Email é Obrigatório.");
            RuleFor(x => x.Phone).NotNull().NotEmpty().WithMessage("O campo Telefone é Obrigatório.");
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("O campo Descrição é Obrigatório.");
            RuleFor(x => x.Cnpj).NotNull().NotEmpty().WithMessage("O campo CNPJ é Obrigatório.");
            RuleFor(x => x.AdoptionRate).GreaterThanOrEqualTo(0).WithMessage("O Taxa de Adoção deve ser maior ou igual à 0.");
        }
    }
}
