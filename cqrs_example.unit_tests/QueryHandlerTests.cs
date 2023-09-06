using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Moq;

namespace cqrs_example.unit_tests;

public class QueryHandlerTests
{
    private IQueryHandler _queryHandler;
    private Guid _personId = Guid.NewGuid();
    private Person _person;

    public QueryHandlerTests()
    {
        var options = new DbContextOptionsBuilder<CQRSDBContext>()
            .UseInMemoryDatabase(databaseName: "PeopleDBInMemory")
            .Options;

        var logger = new Mock<ILogger<IQueryHandler>>();
        var context = new CQRSDBContext(options);
        _person = new Person{
            Id = _personId,
            GivenName = "Jordan",
            Surname = "Brown",
            Gender = Gender.Male,
            BirthDate = DateTime.Now,
            BirthLocation = "USA",
            DeathDate = null,
            DeathLocation = string.Empty
        };
        context.Add(_person);

        // Add some test people
        for (int i = 0; i >= 10 ; i++)
        {
            _person.Id = Guid.NewGuid();
            context.People.Add(_person);    
        }
        context.SaveChangesAsync();
        _queryHandler = new QueryHandler(context, logger.Object);
    }

    [Fact]
    public async void TestGetAllPeopleQuery()
    {
        var query = new GetAllPeopleQuery();

        List<Person>? people = await _queryHandler.HandleGetAllPeopleQuery(query);
        Assert.NotNull(people);
        Assert.True(people.Contains(_person));
        foreach (var person in people)
        {
            Assert.NotNull(person.Id);
            Assert.NotNull(person.GivenName);
            Assert.NotNull(person.Surname);
            Assert.NotNull(person.Gender);
            Assert.NotNull(person.BirthDate);
            Assert.NotNull(person.BirthLocation);
        }
    }

    [Fact]
    public async void TestGetPersonById()
    {
        var query = new GetPersonByIdQuery(_personId);

        Person? person = await _queryHandler.HandleGetPersonById(query);
        Assert.Equal(_person, person);
    }
}
