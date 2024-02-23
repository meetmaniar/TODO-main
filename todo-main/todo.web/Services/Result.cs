using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace todo.web.Services
{
    public class Result
    {
        public HttpStatusCode ResponseStatus { get; protected set; }

        public bool IsSuccessStatusCode { get; protected set; }

        public ProblemDetails? Problem { get; protected set; }

        public string? RawText { get; protected set; }

        public static async Task<Result> Read(HttpResponseMessage response) => 
            new Result
            {
                Problem = response.StatusCode == HttpStatusCode.BadRequest ? await response.Content.ReadFromJsonAsync<ProblemDetails>() : null,
                ResponseStatus = response.StatusCode,
                RawText = await response.Content.ReadAsStringAsync(),
                IsSuccessStatusCode = response.IsSuccessStatusCode
            };
    }
}
