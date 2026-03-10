using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ValidationException = FluentValidation.ValidationException;

namespace VetPawPlatform.Domain.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = exception switch
        {            
            ValidationException ex => CreateValidationProblem(httpContext, ex),

            DomainException ex => CreateProblem(
                httpContext,
                HttpStatusCode.UnprocessableEntity,
                "Regra de negócio violada",
                ex.Message
            ),

            NotFoundException ex => CreateProblem(
                httpContext,
                HttpStatusCode.NotFound,
                "Recurso não encontrado",
                ex.Message
            ),

            _ => CreateProblem(
                httpContext,
                HttpStatusCode.InternalServerError,
                "Erro interno inesperado",
                "Ocorreu um erro interno inesperado."
            )
        };

        httpContext.Response.StatusCode = problemDetails.Status!.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private static ProblemDetails CreateProblem(HttpContext context, HttpStatusCode statusCode, string title, string detail)
    {
        return new ProblemDetails
        {
            Title = title,
            Detail = detail,
            Status = (int)statusCode,
            Instance = context.Request.Path,
            Extensions =
            {
                ["traceId"] = context.TraceIdentifier,
                ["timestamp"] = DateTime.UtcNow
            }
        };
    }

    private static ValidationProblemDetails CreateValidationProblem(HttpContext context, ValidationException ex)
    {
        var errors = ex.Errors
            .GroupBy(validation => validation.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group.Select(validation => validation.ErrorMessage).ToArray()
            );

        return new ValidationProblemDetails(errors)
        {
            Title = "Erro de validação",
            Status = StatusCodes.Status400BadRequest,
            Instance = context.Request.Path,
            Extensions =
            {
                ["traceId"] = context.TraceIdentifier,
                ["timestamp"] = DateTime.UtcNow
            }
        };
    }
}