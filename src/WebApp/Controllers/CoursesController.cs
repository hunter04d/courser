using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Courses;
using Application.Courses.Commands;
using Application.Courses.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CoursesController : ApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id) =>
            Ok(await Mediator.Send(new GetCourse(id)));

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await Mediator.Send(new GetAllCourses()));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseInput input)
        {
            var dto = await Mediator.Send(new CreateCourse(input));
            return CreatedAtAction(nameof(Get), new {dto.Id}, dto);
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CourseDto dto)
        {
            await Mediator.Send(new UpdateCourse(id, dto));
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteCourse(id));
            return Ok();
        }
    }
}
