using System;
using System.Collections.Generic;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApp.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            switch (exception)
            {
                case NotFoundException notFoundException:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                    context.Result = new NotFoundObjectResult(new ValidationProblemDetails(
                        new Dictionary<string, string[]>
                        {
                            [""] = new[] {notFoundException.Message},
                        }));
                    return;
                case BadRequestException badRequestException:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    var problems = new ValidationProblemDetails(badRequestException.Errors);
                    context.Result =
                        new BadRequestObjectResult(problems);
                    return;
            }
        }
    }
}
