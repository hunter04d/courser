using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Exceptions;
using Domain.Entities;
using MediatR;

namespace Application.Courses.Commands
{
    public class DeleteCourse : IRequest<Unit>
    {
        public Guid Id { get; }

        public DeleteCourse(Guid id) => Id = id;

        public class Handler : IRequestHandler<DeleteCourse>
        {
            private readonly IAppDbContext _dbContext;

            public Handler(IAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Unit> Handle(DeleteCourse request, CancellationToken cancellationToken)
            {
                var course = await _dbContext.Courses.FindAsync(request.Id, cancellationToken) ??
                             throw new NotFoundException(nameof(Course), request.Id);
                _dbContext.Courses.Remove(course);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
