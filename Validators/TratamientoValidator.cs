using Farma_plus.Models;
using FluentValidation;

namespace Farma_plus.Validators
{
    public class TratamientoValidator : AbstractValidator<Tratamiento>
    {
        public TratamientoValidator()
        {
            RuleFor(x => x.NoConsulta).NotNull().NotEmpty();
            RuleFor(x => x.ClaveMedicamento).NotNull().NotEmpty();
            RuleFor(x => x.Periodo).GreaterThanOrEqualTo(2023).LessThan(2050).NotNull().NotEmpty();
        }
    }
}
