using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Courses.Queries
{
    public class GetAllCourses : IRequest<IEnumerable<CourseDto>>
    {
        public class Handler : IRequestHandler<GetAllCourses, IEnumerable<CourseDto>>
        {
            private readonly IAppDbContext _dbContext;

            public Handler(IAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public Task<IEnumerable<CourseDto>> Handle(GetAllCourses request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_dbContext.Courses.AsNoTracking().Select(course => new CourseDto
                {
                    Id = course.Id,
                    Name = course.Name,
                    DayOfWeek = course.DayOfWeek,
                    Price = course.Price,
                    StartTime = course.StartTime,
                    EndTime = course.EndTime,
                }).AsEnumerable());
            }
        }
    }
}
