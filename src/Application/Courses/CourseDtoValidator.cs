using FluentValidation;

namespace Application.Courses
{
    public class CourseDtoValidator : AbstractValidator<CourseDto>
    {
        public CourseDtoValidator() => Include(new CourseInputValidator());
    }
}
