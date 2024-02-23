using Microsoft.AspNetCore.Mvc;
using todo.web.Filters;
using todo.web.Resources;
using todo.web.Services;

namespace todo.web.Controllers
{
    [TodoUserRequired]
    public class BaseController : Controller
    {
        protected ITodoClient _client;

        protected string UserName => HttpContext.Session.GetString(AppSettings.Instance.TodoUserHeader) ?? "";

        public BaseController(ITodoClient client)
        {
            _client = client;
        }

        protected void SetSuccessMessage(string message = "")
        {
            if (message == "") message = Labels.ChangesSavedSuccessfully;

            TempData["Success"] = message;
        }
    }
}
