using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayApi.Middleware
{
    /// <summary>
    /// Represents middleware for logging request and response
    /// </summary>
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private const string X_CORRELATION_ID = "X-CorrelationId";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next">next</param>
        /// <param name="logger">logger</param>
        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            StringValues header = httpContext.Request.Headers[X_CORRELATION_ID];
            string correlationId;

            if (header.Count > 0)
            {
                correlationId = header[0];
            }
            else
            {
                correlationId = Guid.NewGuid().ToString();
                httpContext.Request.Headers.Add(X_CORRELATION_ID, correlationId);
            }

            _logger.LogInformation(await FormatRequest(httpContext.Request, correlationId));
            httpContext.Request.Body.Seek(0, SeekOrigin.Begin);

            Stream originalBodyStream = httpContext.Response.Body;

            using MemoryStream responseBody = new MemoryStream();
            httpContext.Response.Body = responseBody;
            
            await _next(httpContext);
            
            httpContext.Response.Headers.Add(X_CORRELATION_ID, correlationId);

            _logger.LogInformation(await FormatResponse(httpContext.Response, correlationId));
            await responseBody.CopyToAsync(originalBodyStream);
        }

        private async Task<string> FormatRequest(HttpRequest request, string correlationId)
        {
            request.EnableBuffering();
            Stream body = request.Body;

            byte[] buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer.AsMemory(0, buffer.Length));
            string bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body = body;

            return $"Request: {correlationId}|{DateTime.Now}|{request.Scheme}|{request.Host}|{request.Path}|{request.ContentType}|{request.QueryString}|{bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response, string correlationId)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return $"Response: {correlationId}|{DateTime.Now}|{text}";
        }
    }
}
