using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Courses.Commands
{
    public class CreateCourse : IRequest<Course>
    {
        public CreateCourse(CourseInput input) => Input = input;
        public CourseInput Input { get; }

        public class Handler : IRequestHandler<CreateCourse, Course>
        {
            private readonly IAppDbContext _dbContext;

            public Handler(IAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Course> Handle(CreateCourse request, CancellationToken cancellationToken)
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
                return course;
            }
        }

        public class Validator : AbstractValidator<CreateCourse>
        {
            public Validator() => RuleFor(c => c.Input).SetValidator(new CourseInputValidator());
        }
    }
}
