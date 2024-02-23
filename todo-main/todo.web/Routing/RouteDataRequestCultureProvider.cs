using Microsoft.AspNetCore.Localization;

namespace todo.web.Routing
{
    public class RouteDataRequestCultureProvider : RequestCultureProvider
    {
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var culture = httpContext.Request?.Path.Value?.Split('/')[1] ?? string.Empty;
            if (culture != string.Empty && Options?.SupportedCultures?.Any(x => x.TwoLetterISOLanguageName == culture) == true)
            {
                httpContext.Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, culture);
            }
            else
            {
                culture = httpContext.Request?.Cookies[CookieRequestCultureProvider.DefaultCookieName];
            }

            var provider = Options?.SupportedCultures?
                .Any(x => x.TwoLetterISOLanguageName.Equals(culture, StringComparison.InvariantCultureIgnoreCase)) ?? false
                ? new ProviderCultureResult($"{culture}-CA")
                : new ProviderCultureResult(DefaultCulture);

            return Task.FromResult(provider);
        }

        private string DefaultCulture => Options?.DefaultRequestCulture.Culture.TwoLetterISOLanguageName ?? string.Empty;
    }
}
