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

        private EmployeeCreateDto BaseValidDto() => new()
        {
            Name = "Alice",
            Email = "alice@example.com",
            LicensePlates = ["AA-11-AA"]
        };

        [Test]
        public void Name_Empty_ShouldFail()
        {
            var dto = BaseValidDto();
            dto.Name = "";

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void Name_LessThan3_ShouldFail()
        {
            var dto = BaseValidDto();
            dto.Name = "Al";

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void Name_TooLong_ShouldFail()
        {
            var dto = BaseValidDto();
            dto.Name = new string('A', 51);

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void Name_Valid_ShouldPass()
        {
            var dto = BaseValidDto();

            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void Email_Empty_ShouldFail()
        {
            var dto = BaseValidDto();
            dto.Email = "";

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Test]
        public void Email_InvalidFormat_ShouldFail()
        {
            var dto = BaseValidDto();
            dto.Email = "not-an-email";

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Test]
        public void Email_TooLong_ShouldFail()
        {
            var dto = BaseValidDto();
            dto.Email = new string('a', 101) + "@test.com";

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Test]
        public void Email_Valid_ShouldPass()
        {
            var dto = BaseValidDto();
            dto.Email = "valid@example.com";

            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        [Test]
        public void LicensePlates_Null_ShouldFail()
        {
            var dto = BaseValidDto();
            dto.LicensePlates = null;

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.LicensePlates);
        }

        [Test]
        public void LicensePlates_Empty_ShouldFail()
        {
            var dto = BaseValidDto();
            dto.LicensePlates = [];

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.LicensePlates);
        }

        [Test]
        public void LicensePlates_Duplicates_ShouldFail()
        {
            var dto = BaseValidDto();
            dto.LicensePlates = ["AA-11-AA", "AA-11-AA"];

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.LicensePlates);
        }

        [Test]
        public void LicensePlates_TooMany_ShouldFail()
        {
            var dto = BaseValidDto();
            dto.LicensePlates = ["AA-11-AA", "BB-22-BB", "CC-33-CC", "DD-44-DD"];

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.LicensePlates);
        }

        [Test]
        public void LicensePlates_ValidCount_ShouldPass()
        {
            var dto = BaseValidDto();
            dto.LicensePlates = ["AA-11-AA", "BB-22-BB", "CC-33-CC"];

            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void LicensePlate_InvalidFormat_ShouldFail()
        {
            var dto = BaseValidDto();
            dto.LicensePlates = ["INVALID"];

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor("LicensePlates[0]");
        }

        [Test]
        public void LicensePlate_TooLong_ShouldFail()
        {
            var dto = BaseValidDto();
            dto.LicensePlates = ["AA-11-AAAA"];

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor("LicensePlates[0]");
        }

        [Test]
        public void LicensePlate_Empty_ShouldFail()
        {
            var dto = BaseValidDto();
            dto.LicensePlates = [""];

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor("LicensePlates[0]");
        }

        [Test]
        public void LicensePlate_Valid_ShouldPass()
        {
            var dto = BaseValidDto();
            dto.LicensePlates = ["AA-11-AA"];

            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
