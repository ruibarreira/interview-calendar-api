using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;

namespace InterviewCalendarAPI
{
    public class HttpContextMiddleware
    {
        const string MessageTemplate =
            "HTTP {Method} {Path} responded {StatusCode} in {Elapsed:0.0000} ms [User:{user}][Protocol:{Protocol}][Host:{Host}][Referer:{Referer}][User-Agent:{UserAgent}]";

        const string ErrorMessageTemplate =
            "HTTP {Method} {Path} responded {StatusCode} in {Elapsed:0.0000} ms [User:{user}][Protocol:{Protocol}][Host:{Host}][Headers:{requestHeaders}]";


        readonly RequestDelegate _next;

        public HttpContextMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            var user = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : "unknown";
            var requestHeaders = httpContext.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString());

            // Add custom response headers
            httpContext.Response.OnStarting(() =>
            {
                httpContext.Response.Headers.Add("lastCallDate", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                return Task.CompletedTask;
            });

            var sw = Stopwatch.StartNew();
            try
            {
                await _next(httpContext);
                sw.Stop();
                Log.Information(
                    MessageTemplate,
                    httpContext.Request.Method,
                    httpContext.Request.Path,
                    httpContext.Response.StatusCode,
                    sw.Elapsed.TotalMilliseconds,
                    user,
                    httpContext.Request.Protocol,
                    httpContext.Request.Host,
                    requestHeaders.ContainsKey("Referer") ? requestHeaders["Referer"] : "",
                    requestHeaders.ContainsKey("User-Agent") ? requestHeaders["User-Agent"] : ""
                );
            }
            catch (Exception ex)
            {
                sw.Stop();
                var errorId = Guid.NewGuid();
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                Log.Error(ex,
                    ErrorMessageTemplate,
                    httpContext.Request.Method,
                    httpContext.Request.Path,
                    httpContext.Response.StatusCode,
                    sw.Elapsed.TotalMilliseconds,
                    user,
                    httpContext.Request.Protocol,
                    httpContext.Request.Host,
                    requestHeaders,
                    errorId
                );

                int statusCode = 500;
                string message = $"errorId: {errorId}";
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new { statusCode, message }));
            }
        }
    }
}
