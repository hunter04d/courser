using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Exceptions;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Courses.Commands
{
    public class UpdateCourse : IRequest<Unit>
    {
        public Guid Id { get; }
        public CourseDto Input { get; }

        public UpdateCourse(Guid id, CourseDto input) => (Id, Input) = (id, input);

        public void Deconstruct(out Guid id, out CourseDto input) => (id, input) = (Id, Input);

        public class Handler : IRequestHandler<UpdateCourse>
        {
            private readonly IAppDbContext _dbContext;

            public Handler(IAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Unit> Handle(UpdateCourse request, CancellationToken cancellationToken)
            {
                var (id, courseDto) = request;
                if (id != courseDto.Id) return Unit.Value;

                var course = await _dbContext.Courses.FindAsync(id) ?? throw new NotFoundException(nameof(Course), id);

                course.DayOfWeek = courseDto.DayOfWeek;
                course.Name = courseDto.Name;
                course.StartTime = courseDto.StartTime;
                course.EndTime = courseDto.EndTime;
                course.Price = courseDto.Price;

                _dbContext.Courses.Update(course);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }

        public class Validator : AbstractValidator<UpdateCourse>
        {
            public Validator()
            {
                RuleFor(c => c.Input).SetValidator(new CourseDtoValidator());
            }
        }
    }
}
