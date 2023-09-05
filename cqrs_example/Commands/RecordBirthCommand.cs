namespace cqrs_example.Commands;

public class RecordBirthCommand
{
    public RecordBirthCommand(DateTime birthDate, string birthLocation)
    {
        BirthDate = birthDate;
        BirthLocation = birthLocation;
    }

    public DateTime? BirthDate { get; }
    public string BirthLocation { get; }
}
