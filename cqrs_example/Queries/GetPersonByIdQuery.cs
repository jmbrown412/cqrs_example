namespace cqrs_example;

public class GetPersonByIdQuery
{
    public GetPersonByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
