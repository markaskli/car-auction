using CarAuction.Api.Common.Interfaces;

namespace CarAuction.Api.Common
{
    public static class ApiResults
    {
        public static IResult Problem(Result result)
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException();
            }

            static string GetTitle(Error error) =>
                error.Type switch
                {
                    ErrorType.Problem => "Invalid request",
                    ErrorType.NotFound => "Resource not found",
                    ErrorType.Forbidden => "Forbidden",
                    ErrorType.Validation => "One or more validation errors occurred",
                    _ => "Internal server error"
                };

            static string GetDetails(Error error) =>
                error.Type switch
                {
                    ErrorType.Problem => error.Message,
                    ErrorType.NotFound => error.Message,
                    ErrorType.Forbidden => error.Message,
                    ErrorType.Validation => error.Message,
                    _ => "An unexpected error occurred"
                };

            static string GetType(ErrorType errorType) =>
                errorType switch
                {
                    ErrorType.Problem => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.6.4",
                    ErrorType.Forbidden => "https://tools.ietf.org/html/rfc7231#section-6.5.3",
                    ErrorType.Validation => "https://tools.ietf.org/html/rfc4918#section-11.2",
                    _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
                };

            static int GetStatusCode(ErrorType errorType) =>
                errorType switch
                {
                    ErrorType.Problem => StatusCodes.Status400BadRequest,
                    ErrorType.NotFound => StatusCodes.Status404NotFound,
                    ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                    ErrorType.Validation => StatusCodes.Status422UnprocessableEntity,
                    _ => StatusCodes.Status500InternalServerError
                };

            return Results.Problem(
                title: GetTitle(result.Error),
                detail: GetDetails(result.Error),
                type: GetType(result.Error.Type),
                statusCode: GetStatusCode(result.Error.Type));
        }
    }
}
