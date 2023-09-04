using cqrs_example.Commands;
using Data;
using Microsoft.EntityFrameworkCore;

namespace cqrs_example.unit_tests;

public class CommandHandlerTests
{
    private ICommandHandler _commandHandler;

    public CommandHandlerTests()
    {
        var options = new DbContextOptionsBuilder<CQRSDBContext>()
            .UseInMemoryDatabase(databaseName: "PeopleDBInMemory")
            .Options;

        ICommandValidator validator = new CommandValidator();
        var context = new CQRSDBContext(options);
        _commandHandler = new CommandHandler(context, validator);
    }

    [Fact]
    public async void TestCreatePersonCommandSuccess()
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

        var person = await _commandHandler.HandleCreatePerson(command);
        Assert.NotNull(person.Id);
        Assert.Equal(command.GivenName, person.GivenName);
        Assert.Equal(command.Surname, person.Surname);
        Assert.Equal(command.Gender, person.Gender);
        Assert.Equal(command.BirthDate, person.BirthDate);
        Assert.Equal(command.BirthLocation, person.BirthLocation);
        Assert.Equal(command.DeathDate, person.DeathDate);
        Assert.Equal(command.DeathLocation, person.DeathLocation);
    }
}
