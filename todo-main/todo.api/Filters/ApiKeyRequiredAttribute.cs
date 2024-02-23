using Microsoft.AspNetCore.Mvc.Filters;
using todo.api.common;
using todo.api.Controllers;
using todo.api.Services;

namespace todo.api.Filters
{
    public class ApiKeyRequiredAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (BaseController)context.Controller;
            var apikey = context.HttpContext.Request.Headers[AppSettings.Instance.ApiKeyHeader].FirstOrDefault();
            if (apikey == null)
            {
                context.Result = controller.Unauthorized();
                return;
            }

            var services = context.HttpContext.RequestServices.GetRequiredService<TodoServices>();
            var client = services.ClientServices.Get(apikey).Result;
            if (client == null)
            {
                context.Result = controller.Unauthorized("Invalid API key");
                return;
            }

            controller.ClientDetails = client;
        }
    }
}
