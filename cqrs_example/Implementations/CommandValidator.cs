using cqrs_example.Commands;

namespace cqrs_example;

public class CommandValidator : ICommandValidator
{
    public bool ValidateCreatePersonCommand(CreatePersonCommand command)
    {
        bool valid = true;

        if (string.IsNullOrEmpty(command.GivenName) || string.IsNullOrEmpty(command.Surname)){
            valid = false;
        }
        else if (command.Surname == null)
        {
            valid = false;
        }

        return valid;
    }

    public bool ValidateUpdatePersonCommand(RecordBirthCommand command)
    {
        bool valid = true;

        if (command.BirthDate == null || command.BirthLocation == null){
            valid = false;
        }

        return valid;
    }
}
