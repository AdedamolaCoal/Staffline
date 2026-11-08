using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using ValidationException = Staffline.Application.Common.Exceptions.ValidationException;

namespace Staffline.Api.Common;

public sealed class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var argument in context.ActionArguments.Values)
        {
            if (argument is null) continue;

            var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
            if (context.HttpContext.RequestServices.GetService(validatorType) is not IValidator validator) continue;

            var result = await validator.ValidateAsync(
                new ValidationContext<object>(argument), context.HttpContext.RequestAborted
            );

            if (!result.IsValid)
                throw new ValidationException(result.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(d
                            => d.Key,
                        d => d.Select(e => e.ErrorMessage).ToArray()));
        }

        await next();
    }
}