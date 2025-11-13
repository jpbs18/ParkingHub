using FluentValidation;
using ParkingHub.Data.DTOs.Employee;

namespace ParkingHub.Validators
{
    public class EmployeeCreateDtoValidator : AbstractValidator<EmployeeCreateDto>
    {
        public EmployeeCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters.")
                .MaximumLength(50).WithMessage("Name must be at most 50 characters.");

            RuleFor(x => x.LicensePlates)
                .NotNull().WithMessage("License plates list cannot be null.");

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(100).WithMessage("Email must not exceed 100 characters.")
                .EmailAddress().WithMessage("Invalid email format.")
                .WithMessage("Email already exists.");

            When(x => x.LicensePlates != null, () =>
            {
                RuleFor(x => x.LicensePlates)
                    .NotEmpty()
                    .WithMessage("At least one license plate is required.");

                RuleFor(x => x.LicensePlates)
                    .Must(plates => plates.Distinct().Count() == plates.Count)
                    .WithMessage("License plates must be unique.");

                RuleFor(x => x.LicensePlates)
                    .Must(plates => plates.Count <= 3)
                    .WithMessage("An employee can have at most 3 license plates.");

                RuleForEach(x => x.LicensePlates)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Plate number cannot be empty.")
                    .MaximumLength(8).WithMessage("Plate number must be at most 8 characters.")
                    .Matches(@"^[A-Z0-9]{2}-[A-Z0-9]{2}-[A-Z0-9]{2}$")
                    .WithMessage("Each plate must be in format XX-XX-XX with letters or numbers.");
            });
        }
    }
}
