using System;
using System.Linq;
using Common;
using FluentValidation;

namespace Application.Courses
{
    public class CourseInputValidator : AbstractValidator<CourseInput>
    {
        private static readonly DayOfWeek[] ValidDays =
            {DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday};

        public CourseInputValidator()
        {
            RuleFor(course => course.DayOfWeek).Must(day => ValidDays.Any(d => d == day))
                .WithMessage("dayOfWeek must be work a work day");
            RuleFor(course => course.Name).NotEmpty().NotNull().MaximumLength(400);
            RuleFor(course => course.StartTime).SetValidator(new TimeOfDayValidator()).NotNull();
            RuleFor(course => course.EndTime).SetValidator(new TimeOfDayValidator()).NotNull();
            RuleFor(course => course.Price).GreaterThanOrEqualTo(0);

            RuleFor(course => course).Must(course => course.StartTime < course.EndTime)
                .WithMessage("Courses start time must be before it's end time");
        }
    }
}
