using cqrs_example.Commands;

namespace cqrs_example.unit_tests;

public class ValidationTests
{
    private ICommandValidator _validator;

    public ValidationTests()
    {
        _validator = new CommandValidator();
    }

    [Fact]
    public void TestValidateCreatePersonCommandSuccess()
    {
        var command = new CreatePersonCommand(
            "Jay",
            "Bee",
            Models.Gender.Male,
            new DateTime(1983, 04, 12),
            "USA",
            null,
            string.Empty
        );

        bool valid = _validator.ValidateCreatePersonCommand(command);
        Assert.True(valid);
    }
}