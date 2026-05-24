using FluentValidation;
using Mediator;

namespace CarAuction.Api.Common
{
    public sealed class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async ValueTask<TResponse> Handle(TRequest message, MessageHandlerDelegate<TRequest, TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(message);

            var validationFailures = await Task.WhenAll(
                _validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

            var errors = validationFailures
                .Where(validationResult => !validationResult.IsValid)
                .SelectMany(validationResult => validationResult.Errors)
                .ToList();

            if (errors.Count > 0)
            {
                throw new ValidationException(errors);
            }

            var response = await next(message, cancellationToken);

            return response;
        }
    }
}
