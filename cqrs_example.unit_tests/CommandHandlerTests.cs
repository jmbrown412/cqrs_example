using cqrs_example.Commands;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Moq;

namespace cqrs_example.unit_tests;

public class CommandHandlerTests
{
    private ICommandHandler _commandHandler;
    private Guid _personId = Guid.NewGuid();

    public CommandHandlerTests()
    {
        var options = new DbContextOptionsBuilder<CQRSDBContext>()
            .UseInMemoryDatabase(databaseName: "PeopleDBInMemory")
            .Options;

        ICommandValidator validator = new CommandValidator();
        var logger = new Mock<ILogger<ICommandHandler>>();
        var context = new CQRSDBContext(options);
        Person person = new Person{
            Id = _personId,
            GivenName = "Jordan",
            Surname = "Brown",
            Gender = Gender.Male,
            BirthDate = DateTime.Now,
            BirthLocation = "USA",
            DeathDate = null,
            DeathLocation = string.Empty
        };
        context.People.Add(person);
        context.SaveChangesAsync();
        _commandHandler = new CommandHandler(context, validator, logger.Object);
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

    [Fact]
    public async void TestUpdatePersonCommandSuccess()
    {
        var command = new RecordBirthCommand(
            new DateTime(1983, 04, 12),
            "New birth location"
        );

        var person = await _commandHandler.HandleUpdatePerson(_personId, command);
        Assert.NotNull(person.Id);
        Assert.Equal(command.BirthDate, person.BirthDate);
        Assert.Equal(command.BirthLocation, person.BirthLocation);
    }
}
