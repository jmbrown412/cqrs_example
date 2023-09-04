using cqrs_example.Commands;

namespace cqrs_example;

public class CommandValidator : ICommandValidator
{
    public bool ValidateCreatePersonCommand(CreatePersonCommand command)
    {
        bool valid = true;

        if (string.IsNullOrEmpty(command.GivenName)){
            valid = false;
        }
        else if (string.IsNullOrEmpty(command.Surname))
        {
            valid = false;
        }

        return valid;
    }
}
