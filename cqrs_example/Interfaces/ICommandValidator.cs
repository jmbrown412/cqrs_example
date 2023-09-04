using cqrs_example.Commands;

namespace cqrs_example;

public interface ICommandValidator
{
    bool ValidateCreatePersonCommand(CreatePersonCommand command);
}
