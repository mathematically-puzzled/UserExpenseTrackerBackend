using Application.Pipeline_Behaviour.Contracts;
using FluentValidation;
using MediatR;

namespace Application.Pipeline_Behaviour
{
    public class ValidatorPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, IValidate
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidatorPipelineBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                List<string> errors = new();

                var ValidationResults = await Task
                    .WhenAll(
                    _validators
                    .Select(x => x.ValidateAsync(context, cancellationToken)));

                var Failures = ValidationResults.SelectMany(x => x.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (Failures.Count != 0)
                {
                    foreach (var failure in Failures)
                    {
                        errors.Add(failure.ErrorMessage);
                    }

                    var errorMessage = string.Join(Environment.NewLine, errors);
                    throw new Exception(errorMessage);
                }

            }

            return await next();
        }
    }
}
