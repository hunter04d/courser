using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Courses;
using Application.Courses.Commands;
using Common;
using Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Application.Test.Courses.Commands
{
    public class CreateCourseTests : CommandsTestsBase
    {
        private readonly CreateCourse.Handler _handler;

        public CreateCourseTests()
        {
            _handler = new CreateCourse.Handler(DbContext);
        }


        [Fact]
        async Task CreateCourseHandler_ShouldAddItemToDatabase()
        {
            var input = new CourseInput
            {
                Name = "Test",
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeOfDay(0, 0),
                EndTime = new TimeOfDay(12, 12),
            };
            await _handler.Handle(new CreateCourse(input), CancellationToken.None);
            DbContext.Courses.AsEnumerable().Should().NotBeEmpty();
            var course = DbContext.Courses.SingleOrDefault();
            course.Should().NotBeNull();
            course?.Name.Should().Be(input.Name);
        }
    }
}
