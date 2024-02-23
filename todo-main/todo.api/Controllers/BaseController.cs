using Microsoft.AspNetCore.Mvc;
using todo.api.Filters;
using todo.api.Services;
using todo.data.Entities;

namespace todo.api.Controllers
{
    [ApiKeyRequired, TodoUserRequired, ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly TodoServices _services;

        public ClientApplication ClientDetails { get; set; } = default!;

        public TodoUser TodoUser { get; set; } = default!;

        public BaseController(TodoServices services)
        {
            _services = services;
        }
    }
}
