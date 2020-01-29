using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CoursesController : ApiController
    {

        public CoursesController()
        {
        }

        [HttpGet("{id}")]
        public Task<IActionResult> Get([FromRoute] Guid id)
        {
            return Task.FromResult<IActionResult>(NoContent());
        }

        [HttpGet]
        public Task<IActionResult> GetAll()
        {
            return Task.FromResult<IActionResult>(NoContent());
        }

        [HttpPost]
        public Task<IActionResult> Create()
        {
            return Task.FromResult<IActionResult>(NoContent());
        }

        [HttpPut]
        public Task<IActionResult> Update()
        {
            return Task.FromResult<IActionResult>(NoContent());
        }

        [HttpDelete]
        public Task<IActionResult> Delete()
        {
            return Task.FromResult<IActionResult>(NoContent());
        }
    }
}
