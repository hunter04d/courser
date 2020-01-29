using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Exceptions;
using Domain.Entities;
using MediatR;

namespace Application.Courses.Queries
{
    public class GetCourse : IRequest<Course>
    {
        public Guid Id { get; }

        public GetCourse(Guid courseId) => Id = courseId;

        public class Handler : IRequestHandler<GetCourse, Course>
        {
            private readonly IAppDbContext _dbContext;

            public Handler(IAppDbContext dbContext) => _dbContext = dbContext;

            public async Task<Course> Handle(GetCourse request, CancellationToken cancellationToken) =>
                await _dbContext.Courses.FindAsync(request.Id) ??
                throw new NotFoundException(nameof(Course), request.Id);
        }
    }
}
