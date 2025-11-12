using FluentValidation.TestHelper;
using ParkingHub.Data.DTOs.Employee;
using ParkingHub.Validators;


namespace ParkingHub.Tests.Validators
{
    internal class EmployeeCreateDtoValidatorTests
    {
        private EmployeeCreateDtoValidator _validator = null!;

        [SetUp]
        public void Setup()
        {
            _validator = new EmployeeCreateDtoValidator();
        }

        [Test]
        public void Name_LessThan3Chars_ShouldFail()
        {
            var dto = new EmployeeCreateDto { Name = "Al", LicensePlates = ["AA-11-AA"] };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void Name_TooLong_ShouldFail()
        {
            var dto = new EmployeeCreateDto
            {
                Name = new string('A', 51),
                LicensePlates = ["AA-11-AA"]
            };

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void Name_WithinValidRange_ShouldPass()
        {
            var dto = new EmployeeCreateDto
            {
                Name = "Alice",
                LicensePlates = ["AA-11-AA"]
            };

            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void LicensePlates_Null_ShouldFail()
        {
            var dto = new EmployeeCreateDto { Name = "Alice", LicensePlates = null };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.LicensePlates);
        }

        [Test]
        public void LicensePlates_Empty_ShouldFail()
        {
            var dto = new EmployeeCreateDto { Name = "Alice", LicensePlates = [] };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.LicensePlates);
        }

        [Test]
        public void LicensePlates_TooMany_ShouldFail()
        {
            var dto = new EmployeeCreateDto
            {
                Name = "Alice",
                LicensePlates = ["AA-11-AA", "BB-22-BB", "CC-33-CC", "DD-44-DD"]
            };

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.LicensePlates);
        }

        [Test]
        public void LicensePlates_InvalidFormat_ShouldFail()
        {
            var dto = new EmployeeCreateDto
            {
                Name = "Alice",
                LicensePlates = ["INVALID"]
            };

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor("LicensePlates[0]");
        }

        [Test]
        public void LicensePlates_ValidFormat_ShouldPass()
        {
            var dto = new EmployeeCreateDto
            {
                Name = "Alice",
                LicensePlates = ["AA-11-AA", "BB-22-BB"]
            };

            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void LicensePlates_ExactlyThree_ShouldPass()
        {
            var dto = new EmployeeCreateDto
            {
                Name = "Alice",
                LicensePlates = ["AA-11-AA", "BB-22-BB", "CC-33-CC"]
            };

            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
