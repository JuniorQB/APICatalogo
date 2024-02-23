using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters;

public class ApiLoggingFilter : IActionFilter
{
    private readonly ILogger<ApiLoggingFilter> _logger;

    public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
    {
        this._logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        //Após do metodo Action
        //Antes do metodo Action
        _logger.LogInformation("### Executing -> OnActionExecuted");
        _logger.LogInformation("################################################");
        _logger.LogInformation($"{DateTime.Now.ToShortTimeString()}");
        _logger.LogInformation($"ModelState : {context.ModelState.IsValid}");
        _logger.LogInformation("################################################");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        //Antes do metodo Action
        _logger.LogInformation("### Executed -> OnActionExecuted");
        _logger.LogInformation("################################################");
        _logger.LogInformation($"{DateTime.Now.ToShortTimeString()}");
        _logger.LogInformation($"ModelState : {context.HttpContext.Response.StatusCode}");
        _logger.LogInformation("################################################");
    }

    
}
