using System.IO;
using System.Threading.Tasks;
using DataAuthority.ApplicationService.Commands;
using DataAuthority.Base64Left.API.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DataAuthority.Base64Left.API.Controllers
{
    [Route("v1/diff")]
    public class DataController : Controller
    {
        private readonly IMediator _mediator;

        public DataController(IMediator mediator)
        {
            _mediator = mediator;
        }        

        // POST api/values
        [HttpPost]
        [Route("{id}/Left")]
        //, [FromBody]CreatePayLoadViewModel viewModel
        public async Task<IActionResult> Post(int id, [FromBody] string content)
        {
            if (content == null)
                return BadRequest();            

            CreatePayLoadCommand command = new CreatePayLoadCommand(id, "Left", content);
            bool commandResult = await _mediator.Send(command);

            return commandResult ? CreatedAtAction("Get", new { id = 1 }, null) : (IActionResult)BadRequest();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpGet("Left/{id}")]
        public void Get(int id)
        {
        }
    }
}
