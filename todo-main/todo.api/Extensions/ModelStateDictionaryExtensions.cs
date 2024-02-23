using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace todo.api.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static void AddErrors(this ModelStateDictionary modelState, IDictionary<string, string> errors)
        {
            foreach(var error in errors)
            {
                modelState.AddModelError(error.Key, error.Value);
            }
        }
    }
}
