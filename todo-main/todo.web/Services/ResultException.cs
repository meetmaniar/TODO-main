namespace todo.web.Services
{
    public class ResultException : Exception
    {
        public ResultException(string message, Result result) : base($"{message}. ({result.ResponseStatus}): {result.RawText}")
        { 
        }
    }
}
