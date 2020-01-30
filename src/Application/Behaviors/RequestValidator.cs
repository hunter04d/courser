using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using FluentValidation;
using MediatR;

namespace Application.Behaviors
{
    public class RequestValidator<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidator(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var ctx = new ValidationContext<TRequest>(request);

            var keyErrorPair = _validators.Select(v => v.Validate(ctx))
                .SelectMany(res => res.Errors)
                .Where(e => e != null)
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(grouping => grouping.Key, grouping => grouping.ToArray());
            if (keyErrorPair.Count != 0) throw new BadRequestException(keyErrorPair);

            return next();
        }
    }
}
