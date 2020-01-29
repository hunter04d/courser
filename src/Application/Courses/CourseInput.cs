using System;
using Domain.Entities;

namespace Application.Courses
{
    public class CourseInput
    {
        public DayOfWeek DayOfWeek { get; set; }

        public string Name { get; set; } = null!;

        public TimeOfDay StartTime { get; set; } = null!;

        public TimeOfDay EndTime { get; set; } = null!;

        public decimal Price { get; set; }
    }
}
