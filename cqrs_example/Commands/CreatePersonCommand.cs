using Models;

namespace cqrs_example.Commands;

public class CreatePersonCommand
{
    public CreatePersonCommand(string givenName, string surname, Gender gender, DateTime birthDate, string birthLocation, DateTime? deathDate, string? deathLocation)
    {
        GivenName = givenName;
        Surname = surname;
        Gender = gender;
        BirthDate = birthDate;
        BirthLocation = birthLocation;
        DeathDate = deathDate;
        DeathLocation = deathLocation;
    }

    public string GivenName { get; }   
    public string Surname { get; }
    public Gender Gender { get; }
    public DateTime BirthDate { get; }
    public string BirthLocation { get; }
    public DateTime? DeathDate { get; }
    public string? DeathLocation { get; }
}
