using FluentValidation;
using ParkingHub.Data.DTOs.Employee;

namespace ParkingHub.Validators
{
    public class EmployeeCreateDtoValidator : AbstractValidator<EmployeeCreateDto>
    {
        public EmployeeCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.LicensePlates)
                .NotNull()
                .NotEmpty()
                .Must(lp => lp.Count <= 3)
                .WithMessage("An employee can have at most 3 license plates.");

            RuleForEach(x => x.LicensePlates)
                .NotEmpty()
                .MaximumLength(8)
                .Matches(@"^[A-Z0-9]{2}-[A-Z0-9]{2}-[A-Z0-9]{2}$")
                .WithMessage("Each plate must be in format XX-XX-XX with letters or numbers.");
        }
    }
}
