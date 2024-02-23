using Microsoft.AspNetCore.Mvc;
using System.Net;
using todo.web.Services;

namespace todo.web.test.Util
{
    public class TestResult : Result
    {
        public TestResult(HttpStatusCode status, ProblemDetails? problem = null) 
        {
            ResponseStatus = status;
            RawText = string.Empty;
            Problem = problem;
            IsSuccessStatusCode = (int)status >= 200 && (int)status < 300;
        }
    }
}
