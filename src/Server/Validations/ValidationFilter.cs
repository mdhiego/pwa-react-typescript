using FluentValidation;

namespace BabySounds.Server.Validations;

internal sealed class ValidationFilter<T> : IEndpointFilter where T : class
{
    private readonly IValidator<T> _validator;

    public ValidationFilter(IValidator<T> validator)
    {
        _validator = validator;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var validatable = context.Arguments.SingleOrDefault(static p => p?.GetType() == typeof(T));
        if (validatable is null) return Results.BadRequest("Invalid request.");

        var validationResult = await _validator.ValidateAsync((T)validatable);
        if (validationResult.IsValid) return await next(context);

        var validationErrors = validationResult
            .Errors
            .GroupBy(static x => x.PropertyName)
            .ToDictionary(
                static x => x.Key,
                static x => x.Select(static e => e.ErrorMessage).ToArray()
            );

        return Results.ValidationProblem(validationErrors);

    }
}
