using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace todo.web.Services
{
    public class ObjectResult<T> : Result where T : class
    {
        public T? Value { get; protected set; }

        public static async Task<ObjectResult<T>> ReadObject(HttpResponseMessage response) => 
            new ObjectResult<T>
            {
                Value = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<T>() : null,
                Problem = response.StatusCode == HttpStatusCode.BadRequest ? await response.Content.ReadFromJsonAsync<ProblemDetails>() : null,
                ResponseStatus = response.StatusCode,
                RawText = await response.Content.ReadAsStringAsync(),
                IsSuccessStatusCode = response.IsSuccessStatusCode
            };
    }
}
