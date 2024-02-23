using Microsoft.AspNetCore.Localization;
using System.Globalization;
using todo.web.Routing;

namespace todo.web.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UsePathLocalization(this IApplicationBuilder app)
        {
            app.UseRequestLocalization(o =>
            {
                o.SupportedCultures = o.SupportedUICultures = new[]
                {
                    new CultureInfo("en-CA"),
                    new CultureInfo("fr-CA")
                };

                o.DefaultRequestCulture = new RequestCulture(o.SupportedCultures[0]);
                o.RequestCultureProviders.Insert(0, new RouteDataRequestCultureProvider { Options = o });
            });
        }

        public static void UseLocalizedRouteExceptions(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(o =>
            {
                o.Run(ctx =>
                {
                    var language = ctx.Request.Path.Value.Length >= 3 ? ctx.Request.Path.Value.Substring(1, 2) : "en";
                    if (language.Equals("en")) ctx.Response.Redirect($"{ctx.Request.PathBase.Value}/{language}/error");
                    if (language.Equals("fr")) ctx.Response.Redirect($"{ctx.Request.PathBase.Value}/{language}/erreur");

                    return Task.Delay(0);
                });
            });
        }
    }
}
