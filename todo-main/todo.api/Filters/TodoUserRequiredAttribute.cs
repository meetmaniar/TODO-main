using Microsoft.AspNetCore.Mvc.Filters;
using todo.api.Controllers;
using todo.api.common;
using todo.api.Services;

namespace todo.api.Filters
{
    public class TodoUserRequiredAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controller = (BaseController)context.Controller;
            var username = controller.Request.Headers[AppSettings.Instance.TodoUserHeader].FirstOrDefault();
            if (username == null)
            {
                context.Result = controller.Unauthorized("No user authenticated");
                return;
            }

            var services = context.HttpContext.RequestServices.GetRequiredService<TodoServices>();
            controller.TodoUser = await services.UserServices.Get(username);
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
