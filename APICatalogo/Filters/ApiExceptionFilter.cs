﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters;

public class ApiExceptionFilter : IExceptionFilter
{

    private readonly ILogger<ApiExceptionFilter> logger;
    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
    {

        this.logger = logger;

    }

    public void OnException(ExceptionContext context)
    {
        logger.LogError(context.Exception, "Ocorreu uma exceção não tratada: Status code 500");
        context.Result = new ObjectResult("\"Ocorreu uma exceção não tratada: Status code 500")
        {
            StatusCode = StatusCodes.Status500InternalServerError,
        };

    }
}
