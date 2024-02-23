using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace todo.api.Filters
{
    public class AuthorizationOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var attributes = context.MethodInfo.DeclaringType
                .GetCustomAttributes(true).Concat(context.MethodInfo.GetCustomAttributes(true))
                .OfType<TodoUserRequiredAttribute>();

            if (attributes.Any())
            {
                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "todoUser" }},
                        Array.Empty<string>()
                    }
                });
            }

            operation.Security.Add(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "apikey" }},
                    Array.Empty<string>()
                }
            });
        }
    }
}
