using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Exceptions;
using Domain.Entities;
using MediatR;

namespace Application.Courses.Queries
{
    public class GetCourse : IRequest<CourseDto>
    {
        public Guid Id { get; }

        public GetCourse(Guid courseId)
        {
            Id = courseId;
        }

        public class Handler : IRequestHandler<GetCourse, CourseDto>
        {
            private readonly IAppDbContext _dbContext;

            public Handler(IAppDbContext dbContext) => _dbContext = dbContext;

            public async Task<CourseDto> Handle(GetCourse request, CancellationToken cancellationToken)
            {
                var course = await _dbContext.Courses.FindAsync(request.Id) ??
                             throw new NotFoundException(nameof(Course), request.Id);
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
    }
}
