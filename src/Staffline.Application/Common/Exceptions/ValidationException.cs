namespace Staffline.Application.Common.Exceptions;

public sealed class ValidationException(IReadOnlyDictionary<string, string[]> errors)
    : AppException("One or more validation errors occurred.", "validation_failed")
{
    public IReadOnlyDictionary<string, string[]> Errors = errors;

    public ValidationException(string field, string message)
        : this(new Dictionary<string, string[]>
        {
            [field] = [message]
        })
    {
    }
}