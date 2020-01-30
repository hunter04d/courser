using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Courses;
using Application.Courses.Commands;
using Application.Courses.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CoursesController : ApiController
    {
        /// <summary>
        /// Gets the course by its id
        /// </summary>
        /// <param name="id">The id of the course to get</param>
        /// <returns>The course with an id matching that of and input <paramref name="id"/></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> Get([FromRoute] Guid id) =>
            await Mediator.Send(new GetCourse(id));

        /// <summary>
        /// Returns all of the courses
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAll() =>
            Ok(await Mediator.Send(new GetAllCourses()));

        /// <summary>
        /// Creates the course
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CourseDto), 201)]
        public async Task<IActionResult> Create([FromBody] CourseInput input)
        {
            var dto = await Mediator.Send(new CreateCourse(input));
            return CreatedAtAction(nameof(Get), new {dto.Id}, dto);
        }

        /// <summary>
        /// Update the course with new data
        /// </summary>
        /// <param name="id">the id of the course to update</param>
        /// <param name="dto">The updated course</param>
        ///
        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CourseDto dto)
        {
            await Mediator.Send(new UpdateCourse(id, dto));
            return NoContent();
        }

        /// <summary>
        /// Deletes the course
        /// </summary>
        /// <param name="id">the id of the course to delete</param>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteCourse(id));
            return Ok();
        }
    }
}
