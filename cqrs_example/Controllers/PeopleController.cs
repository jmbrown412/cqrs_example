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

        public PeopleController(ILogger<PeopleController> logger, ICommandHandler commandHandler)
        {
            _logger = logger;
            _commandHandler = commandHandler;
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
    }
}
