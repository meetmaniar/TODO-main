using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace todo.web.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string UserName<T>(this IHtmlHelper<T> html) =>
            html.ViewContext.HttpContext.Session.GetString(AppSettings.Instance.TodoUserHeader) ?? "";

        public static bool IsEnglish<T>(this IHtmlHelper<T> html)
        {
            return html.Language() == "en";
        }

        public static string Language<T>(this IHtmlHelper<T> html)
        {
            var routeData = html.ViewContext.RouteData;
            return (string?)routeData.Values["culture"] ?? "en";
        }

        public static string LanguageLink<T>(this IHtmlHelper<T> html, IUrlHelper urlHelper)
        {
            var routeData = html.ViewContext.RouteData;
            var language = (string?)routeData.Values["culture"] ?? "en";

            var languageToggle = language.Equals("en") ? "fr" : "en";
            var routeDataCopy = new RouteData(routeData);
            routeDataCopy.Values["culture"] = languageToggle;
            var languageLink = urlHelper.RouteUrl(routeDataCopy.Values);
            for (var i = 0; i < html.ViewContext.HttpContext.Request.Query.Count; i++)
            {
                var (key, value) = html.ViewContext.HttpContext.Request.Query.ElementAt(i);
                if (i == 0) languageLink += "?";
                else languageLink += "&";

                languageLink += $"{key}={value}";
            }

            return languageLink ?? "";
        }
    }
}