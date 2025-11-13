using FluentValidation.TestHelper;
using ParkingHub.Data.DTOs.Employee;
using ParkingHub.Validators;

namespace ParkingHub.Tests.Validators
{
    [TestFixture]
    public class EmployeeUpdateDtoValidatorTests
    {
        private EmployeeUpdateDtoValidator _validator = null!;

        [SetUp]
        public void Setup()
        {
            _validator = new EmployeeUpdateDtoValidator();
        }

        private EmployeeUpdateDto BaseValidDto() => new()
        {
            Name = "Alice",
            Email = "alice@example.com",
            InsidePark = false,
            CurrentParkId = null,
            LicensePlates = ["AA-11-AA"]
        };

        [Test]
        public void Name_LessThan3Chars_ShouldFail()
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
        public void Name_WithinValidRange_ShouldPass()
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
        public void InsidePark_IsTrue_CurrentParkId_NotNull_ShouldPass()
        {
            var dto = BaseValidDto();
            dto.InsidePark = true;
            dto.CurrentParkId = 1;

            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveValidationErrorFor(x => x.CurrentParkId);
        }

        [Test]
        public void InsidePark_IsFalse_CurrentParkId_NotNull_ShouldFail()
        {
            var dto = BaseValidDto();
            dto.InsidePark = false;
            dto.CurrentParkId = 2;

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.CurrentParkId);
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
        public void LicensePlates_TooMany_ShouldFail()
        {
            var dto = BaseValidDto();
            dto.LicensePlates = ["AA-11-AA", "BB-22-BB", "CC-33-CC", "DD-44-DD"];

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.LicensePlates);
        }

        [Test]
        public void LicensePlates_InvalidFormat_ShouldFail()
        {
            var dto = BaseValidDto();
            dto.LicensePlates = ["INVALID"];

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor("LicensePlates[0]");
        }

        [Test]
        public void LicensePlates_ValidFormat_ShouldPass()
        {
            var dto = BaseValidDto();
            dto.LicensePlates = ["AA-11-AA", "BB-22-BB"];

            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void LicensePlates_ExactlyThree_ShouldPass()
        {
            var dto = BaseValidDto();
            dto.LicensePlates = ["AA-11-AA", "BB-22-BB", "CC-33-CC"];

            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void LicensePlates_MustBeUnique_ShouldFail()
        {
            var dto = BaseValidDto();
            dto.LicensePlates = ["AA-11-AA", "AA-11-AA"];

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.LicensePlates);
        }
    }
}