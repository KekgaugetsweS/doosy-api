using Doosy.Domain.Interfaces;
using Doosy.Domain.Requests;
using Doosy.Domain.Requests.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Doosy.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        readonly IPersonService service;

        public PersonController(IPersonService service)
        {
            this.service = service;
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(string id)
        {
            var response = service.GetById(id);
            return Ok(response);
        }

        [HttpGet("filter")]
        public IActionResult Filter([FromQuery] PersonFilter filter)
        {
            var response = service.Filter(filter);
            return Ok(response);
        }

        [HttpPut("put")]
        public IActionResult Update([FromBody] PersonRequest request)
        {
            var response = service.Update(request);
            return Ok(response);
        }

        [HttpPost("post")]
        public IActionResult Post([FromBody] PersonRequest request)
        {
            var response = service.Create(request);
            return Ok(response);
        }

        [HttpGet("export")]
        public IActionResult Export([FromQuery] PersonFilter filter)
        {
            var response = service.Export(filter);
            return Ok(response);
        }
    }
}
