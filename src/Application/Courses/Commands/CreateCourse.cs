using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Courses.Commands
{
    public class CreateCourse : IRequest<CourseDto>
    {
        public CreateCourse(CourseInput input) => Input = input;
        public CourseInput Input { get; }

        public class Handler : IRequestHandler<CreateCourse, CourseDto>
        {
            private readonly IAppDbContext _dbContext;

            public Handler(IAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<CourseDto> Handle(CreateCourse request, CancellationToken cancellationToken)
            {
                var courseInput = request.Input;
                var course = new Course
                {
                    DayOfWeek = courseInput.DayOfWeek,
                    Name = courseInput.Name,
                    StartTime = courseInput.StartTime,
                    EndTime = courseInput.EndTime,
                    Price = courseInput.Price
                };
                await _dbContext.Courses.AddAsync(course, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);


                return new CourseDto
                {
                    Id = course.Id,
                    Name = course.Name,
                    DayOfWeek = course.DayOfWeek,
                    Price = course.Price,
                    StartTime = course.StartTime,
                    EndTime = course.EndTime,
                };
            }
        }

        public class Validator : AbstractValidator<CreateCourse>
        {
            public Validator() => RuleFor(c => c.Input).SetValidator(new CourseInputValidator());
        }
    }
}
