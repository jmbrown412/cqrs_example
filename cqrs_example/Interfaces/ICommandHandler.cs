using cqrs_example.Commands;
using Models;

namespace cqrs_example;

public interface ICommandHandler
{
    Task<Person?> HandleCreatePerson(CreatePersonCommand command);
    Task<Person?> HandleUpdatePerson(Guid personId, RecordBirthCommand command);
}
