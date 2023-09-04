using cqrs_example.Commands;
using Data;
using Models;

namespace cqrs_example;

public class CommandHandler : ICommandHandler
{
    private readonly CQRSDBContext _dbContext;
    private readonly ICommandValidator _commandValidator;

    public CommandHandler(CQRSDBContext dbContext, ICommandValidator commandValidator)
    {
        _dbContext = dbContext;
        _commandValidator = commandValidator;
    }

    public async Task<Person?> HandleCreatePerson(CreatePersonCommand command)
    {
        try
        {
            Person person = new Person
            {
                Id = Guid.NewGuid(),
                GivenName = command.GivenName,
                Surname = command.Surname,
                Gender = command.Gender,
                BirthDate = command.BirthDate,
                BirthLocation = command.BirthLocation,
                DeathDate = command.DeathDate,
                DeathLocation = command.DeathLocation,
            };

            _dbContext.People.Add(person);
            await _dbContext.SaveChangesAsync();
            return person;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
