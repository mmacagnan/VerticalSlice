using FluentValidation;
using MediatR;
using VerticalSlice.Abstraction.Messaging;
using ValidationException = VerticalSlice.Exceptions.ValidationException;

namespace VerticalSlice.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>
: IPipelineBehavior<TRequest, TResponse>
where TRequest : class, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    /// <summary>
    /// Fluent validation behavior that initializes
    /// all the fluent validators instances of a request <see cref="TRequest"/>.
    ///
    /// If the validate method return an invalid state, we throw a <see cref="ValidationException"/>
    /// passing all the errors as dictionary
    /// </summary>
    /// <param name="rq"></param>
    /// <param name="next"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public async Task<TResponse> Handle(
        TRequest rq, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken ct)
    {
        if (!_validators.Any())
            return await next();

        var ctx = new ValidationContext<TRequest>(rq);
        var errorsDictionary = _validators
            .Select(x => x.Validate(ctx))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .GroupBy(
                x => x.PropertyName,
                x => x.ErrorMessage,
                (propertyName, errorMessages) => new
                {
                    Key = propertyName,
                    Values = errorMessages.Distinct().ToArray()
                })
            .ToDictionary(x => x.Key, x => x.Values);

        if (errorsDictionary.Any())
            throw new ValidationException(errorsDictionary);

        return await next();
    }
}