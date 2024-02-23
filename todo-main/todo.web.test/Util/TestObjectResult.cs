using Microsoft.AspNetCore.Mvc;
using System.Net;
using todo.web.Services;

namespace todo.web.test.Util
{
    public class TestObjectResult<T> : ObjectResult<T> where T : class
    {
        public TestObjectResult(T? result, HttpStatusCode status = HttpStatusCode.OK, ProblemDetails? problem = null)
        {
            Value = result;
            ResponseStatus = status;
            Problem = problem;
            IsSuccessStatusCode = (int)status >= 200 && (int)status < 300;
        }
    }
}
