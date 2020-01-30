using System;
using Common;

namespace Domain.Entities
{
    public class Course
    {
        public Guid Id { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public string Name { get; set; } = null!;

        /// <summary>
        /// Start time of the course (since midnight)
        /// </summary>
        public TimeOfDay StartTime { get; set; } = null!;

        /// <summary>
        /// End time of the course (since midnight)
        /// </summary>
        public TimeOfDay EndTime { get; set; } = null!;

        /// <summary>
        /// Price of the course
        /// </summary>
        public decimal Price { get; set; }
    }
}
