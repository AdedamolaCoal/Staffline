namespace Staffline.Application.Common.Exceptions;

public sealed class NotFoundException(string entity, object key)
    : AppException($"{entity} {key} was not found.", "not_found");