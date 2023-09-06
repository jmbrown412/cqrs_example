using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace cqrs_example;

public class QueryHandler : IQueryHandler
{
    private readonly CQRSDBContext _dbContext;
    private readonly ILogger<IQueryHandler> _logger;

    public QueryHandler(CQRSDBContext dbContext, ILogger<IQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<Person>?> HandleGetAllPeopleQuery(GetAllPeopleQuery query)
    {
        try
        {
           _logger.LogInformation("Received query to get all people");
           List<Person> people = await _dbContext.People.ToListAsync();
           return people;
        }
        catch (Exception ex)
        {
            _logger.LogError($"There was an error getting all people. {ex.Message}");
            throw;
        } 
    }

    public async Task<Person?> HandleGetPersonById(GetPersonByIdQuery query)
    {
        try
        {
            _logger.LogInformation($"Received query to get person with Id {query.Id}");
           Person? person = await _dbContext.People.FirstOrDefaultAsync(p => p.Id == query.Id);
           return person;
        }
        catch (Exception ex)
        {
            _logger.LogError($"There was an error getting the person with Id {query.Id}. Error: {ex.Message}");
            throw;
        }
    }
}
