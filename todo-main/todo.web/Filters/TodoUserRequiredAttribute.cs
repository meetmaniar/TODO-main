using Microsoft.AspNetCore.Mvc.Filters;
using todo.web.Controllers;
using todo.web.Services;

namespace todo.web.Filters
{
    public class TodoUserRequiredAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controller = (BaseController)context.Controller;
            var username = context.HttpContext.Session.GetString(AppSettings.Instance.TodoUserHeader);
            if (string.IsNullOrWhiteSpace(username))
            {
                username = AppSettings.Instance.DefaultUser;
                context.HttpContext.Session.SetString(AppSettings.Instance.TodoUserHeader, username);
            }

            var client = context.HttpContext.RequestServices.GetRequiredService<ITodoClient>();
            client.Authenticate(username);
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
