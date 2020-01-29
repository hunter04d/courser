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
    public class GetAllCourses : IRequest<IEnumerable<Course>>
    {
        public class Handler : IRequestHandler<GetAllCourses, IEnumerable<Course>>
        {
            private readonly IAppDbContext _dbContext;

            public Handler(IAppDbContext dbContext) => _dbContext = dbContext;

            public Task<IEnumerable<Course>> Handle(GetAllCourses request, CancellationToken cancellationToken) =>
                Task.FromResult(_dbContext.Courses.AsNoTracking().AsEnumerable());
        }
    }
}
