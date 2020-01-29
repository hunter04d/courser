using System;
using Domain.Entities;

namespace Application.Courses
{
    public class CourseDto: CourseInput
    {
        public Guid Id { get; set; }
    }
}
