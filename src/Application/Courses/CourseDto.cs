using System;
using Domain.Entities;

namespace Application.Courses
{
    /// <summary>
    /// Represents the model requited to update
    ///
    /// Note that start time should come before the end time in the day
    /// </summary>
    public class CourseDto : CourseInput
    {
        public Guid Id { get; set; }
    }
}
