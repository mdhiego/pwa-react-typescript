using FluentValidation;

namespace ADS.BabySleepSounds.Server.Features.Common;

internal sealed class ValidationFilter<T> : IEndpointFilter where T : class
{
    private readonly IValidator<T> _validator;

    public ValidationFilter(IValidator<T> validator)
    {
        _validator = validator;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var validatable = context.Arguments.SingleOrDefault(p => p?.GetType() == typeof(T));
        if (validatable is null) return Results.BadRequest("Invalid request.");

        var validationResult = await _validator.ValidateAsync((T)validatable);
        if (!validationResult.IsValid)
        {
            var validationErrors = validationResult
                .Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    x => x.Key,
                    x => x.Select(e => e.ErrorMessage).ToArray()
                );

            return Results.ValidationProblem(validationErrors);
        }

        return await next(context);
    }
}
