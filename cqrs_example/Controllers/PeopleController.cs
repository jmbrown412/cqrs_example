using cqrs_example;
using cqrs_example.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly ILogger<PeopleController> _logger;
        private readonly ICommandHandler _commandHandler;
        private readonly IQueryHandler _queryHandler;

        public PeopleController(ILogger<PeopleController> logger, ICommandHandler commandHandler, IQueryHandler queryHandler)
        {
            _logger = logger;
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreatePersonCommand command)
        {
            var person = await _commandHandler.HandleCreatePerson(command);
            if (person == null)
            {
                return BadRequest();
            }
            return Ok(person);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromQuery] Guid personId, [FromBody] RecordBirthCommand command)
        {
            var person = await _commandHandler.HandleUpdatePerson(personId, command);
            if (person == null)
            {
                return BadRequest();
            }
            return Ok(person);
        }

        [HttpPut]
        public async Task<ActionResult> Get()
        {
            GetAllPeopleQuery query = new GetAllPeopleQuery();
            var people = await _queryHandler.HandleGetAllPeopleQuery(query);
            if (people == null)
            {
                return BadRequest();
            }
            return Ok(people);
        }
    }
}
