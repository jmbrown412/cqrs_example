using cqrs_example.Commands;
using Data;
using Models;

namespace cqrs_example;

public class CommandHandler : ICommandHandler
{
    private readonly CQRSDBContext _dbContext;
    private readonly ICommandValidator _commandValidator;
    private readonly ILogger<ICommandHandler> _logger;

    public CommandHandler(CQRSDBContext dbContext, ICommandValidator commandValidator, ILogger<CommandHandler> logger)
    {
        _dbContext = dbContext;
        _commandValidator = commandValidator;
        _logger = logger;
    }

    public async Task<Person?> HandleCreatePerson(CreatePersonCommand command)
    {
        try
        {
            _logger.LogInformation("Received request to create person");
            bool valid = _commandValidator.ValidateCreatePersonCommand(command);
            if (valid)
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
                _logger.LogInformation($"Successfully created and saved Person with Id {person.Id}");
                return person;
            }
            else
            {
                _logger.LogWarning("CreatePersonCommand failed validation");
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"There was an error creating a person. {ex.Message}");
            throw;
        }
    }
}
