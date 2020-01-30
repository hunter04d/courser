using System;
using Common;
using Domain.Entities;

namespace Application.Courses
{
    /// <summary>
    /// Represent the data required to input new course into the application
    ///
    /// Note that start time should come before the end time in the day
    /// </summary>
    public class CourseInput
    {
        public DayOfWeek DayOfWeek { get; set; }

        public string Name { get; set; } = null!;

        public TimeOfDay StartTime { get; set; } = null!;

        public TimeOfDay EndTime { get; set; } = null!;

        public decimal Price { get; set; }
    }
}
