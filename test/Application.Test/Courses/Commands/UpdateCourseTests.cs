using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Courses;
using Application.Courses.Commands;
using Application.Exceptions;
using Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Application.Test.Courses.Commands
{
    public class UpdateCourseTests : CommandsTestsBase
    {
        private readonly UpdateCourse.Handler _handler;


        private readonly Course _targetCourse;


        private readonly CourseDto _input;

        public UpdateCourseTests()
        {
            _handler = new UpdateCourse.Handler(DbContext);
            var course = new Course
            {
                Name = "Old",
                DayOfWeek = DayOfWeek.Friday,
                StartTime = new TimeOfDay(10, 10),
                EndTime = new TimeOfDay(11, 11),
                Price = 20m,
            };

            DbContext.Add(course);
            DbContext.SaveChanges();
            _targetCourse = course;
            _input = new CourseDto
            {
                Id = _targetCourse.Id,
                Name = "Ok",
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeOfDay(0, 1),
                EndTime = new TimeOfDay(0, 2),
                Price = 10m
            };
        }


        [Fact]
        async Task UpdateCourseHandler_ShouldUpdateCourse_IfIdIsValid()
        {
            await _handler.Handle(new UpdateCourse(_targetCourse.Id, _input), CancellationToken.None);
            _targetCourse.Name.Should().Be(_input.Name);
            _targetCourse.DayOfWeek.Should().Be(_input.DayOfWeek);
            _targetCourse.StartTime.Should().Be(_input.StartTime);
            _targetCourse.EndTime.Should().Be(_input.EndTime);
            _targetCourse.Price.Should().Be(_input.Price);
        }

        [Fact]
        async Task UpdateCourseHandler_ShouldNoop_IfIdsMismatch()
        {
            await _handler.Handle(new UpdateCourse(new Guid(), _input), CancellationToken.None);

            _targetCourse.Name.Should().NotBe(_input.Name);
        }

        [Fact]
        async Task UpdateCourseHandler_ShouldThrow_IsItemIsNotFound()
        {
            _input.Id = new Guid();
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(new UpdateCourse(_input.Id, _input), CancellationToken.None));
        }
    }
}
