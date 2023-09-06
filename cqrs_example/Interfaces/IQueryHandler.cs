using Models;

namespace cqrs_example;

public interface IQueryHandler
{
    Task<List<Person>?> HandleGetAllPeopleQuery(GetAllPeopleQuery query);
    Task<Person?> HandleGetPersonById(GetPersonByIdQuery query);
}
