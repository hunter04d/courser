using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Courses;
using Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Application.Test.Courses
{
    /// <summary>
    /// Characterization tests of the validator
    /// </summary>
    public class CourseInputValidatorTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        void CreateCourseValidator_ShouldValidateProperly(CourseInput input, bool valid)
        {
            var validator = new CourseInputValidator();
            var result = validator.Validate(input);
            result.IsValid.Should().Be(valid);
        }

        public static object[][] Data => new[]
        {
            new object[]
            {
                new CourseInput
                {
                    Name = "Ok",
                    Price = 1.0m,
                    DayOfWeek = DayOfWeek.Monday,
                    StartTime = new TimeOfDay(0, 0),
                    EndTime = new TimeOfDay(1, 0)
                },
                true
            },
            new object[]
            {
                new CourseInput
                {
                    Name = new string('a', 401),
                    Price = 1.0m,
                    DayOfWeek = DayOfWeek.Monday,
                    StartTime = new TimeOfDay(0, 0),
                    EndTime = new TimeOfDay(1, 0)
                },
                false,
            },
            new object[]
            {
                new CourseInput
                {
                    Name = "Ok",
                    Price = -1.0m,
                    DayOfWeek = DayOfWeek.Monday,
                    StartTime = new TimeOfDay(0, 0),
                    EndTime = new TimeOfDay(1, 0)
                },
                false
            },
            new object[]
            {
                new CourseInput
                {
                    Name = "Ok",
                    Price = 1.0m,
                    DayOfWeek = DayOfWeek.Monday,
                    StartTime = new TimeOfDay(124, 0),
                    EndTime = new TimeOfDay(125, 0)
                },
                false
            },
            new object[]
            {
                new CourseInput
                {
                    Name = "Ok",
                    Price = 1.0m,
                    DayOfWeek = DayOfWeek.Sunday,
                    StartTime = new TimeOfDay(12, 0),
                    EndTime = new TimeOfDay(13, 0)
                },
                false
            },
            new object[]
            {
                new CourseInput
                {
                    Name = "Ok",
                    Price = 1.0m,
                    DayOfWeek = DayOfWeek.Monday,
                    StartTime = new TimeOfDay(12, 0),
                    EndTime = new TimeOfDay(11, 0)
                },
                false
            },
        };
    }
}
