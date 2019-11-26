using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using OnlineCourses.Shared.ApplicationExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourses.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;
        public HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(NotFoundException))
            {
                var problemDetails = new ValidationProblemDetails<ErrorMsg>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Value = new ErrorMsg
                    {
                        Message = context.Exception.Message
                    }
                };
                context.Result = new NotFoundObjectResult(problemDetails);
                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            else if (context.Exception.GetType() == typeof(ForbiddenException))
            {
                var problemDetails = new ValidationProblemDetails<ErrorMsg>
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Value = new ErrorMsg
                    {
                        Message = context.Exception.Message
                    }
                };
                context.Result = new ObjectResult(problemDetails);
                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            }
            else if (context.Exception.GetType() == typeof(ConflictOccuredException))
            {
                var problemDetails = new ValidationProblemDetails<ErrorMsg>
                {
                    StatusCode = StatusCodes.Status409Conflict,
                    Value = new ErrorMsg
                    {
                        Message = context.Exception.Message
                    }
                };
                context.Result = new ConflictObjectResult(problemDetails);
                context.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
            }
            else
            {
                var exceptionId = Guid.NewGuid();
                _logger.LogError(context.Exception.StackTrace, exceptionId);
                var problemDetails = new ValidationProblemDetails<ErrorMsg>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Value = new ErrorMsg
                    {
                        Message = $"An error has occured. Please refer to support with this {exceptionId}."
                    }
                };

                context.Result = new InternalServerErrorObjectResult(problemDetails);
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            context.ExceptionHandled = true;
        }
    }

    internal class InternalServerErrorObjectResult : IActionResult
    {
        private ValidationProblemDetails<ErrorMsg> problemDetails;

        public InternalServerErrorObjectResult(ValidationProblemDetails<ErrorMsg> problemDetails)
        {
            this.problemDetails = problemDetails;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.WriteAsync(new ValidationProblemDetails<ErrorMsg>
            {

                StatusCode = StatusCodes.Status500InternalServerError,
                Value = problemDetails.Value
            }.ToString());
            return Task.CompletedTask;
        }
    }

    internal class ValidationProblemDetails<T>
    {
        public int StatusCode { get; set; }

        public T Value { get; set; }
    }

    internal class ErrorMsg
    {
        public string Message { get; set; }
    }


}
