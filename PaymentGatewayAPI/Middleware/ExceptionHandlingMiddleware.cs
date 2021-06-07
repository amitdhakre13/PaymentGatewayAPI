using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace PaymentGatewayApi.Middleware
{
    /// <summary>
    /// Represents exception handling middleware
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next">next</param>
        /// <param name="webHostEnvironment">hostingEnvironment</param>
        public ExceptionHandlingMiddleware(RequestDelegate next, IWebHostEnvironment webHostEnvironment)
        {
            _next = next;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Invoke middleware
        /// </summary>
        /// <param name="httpContext">httpContext</param>
        /// <returns>Exception details</returns>
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(httpContext, _webHostEnvironment, ex).ConfigureAwait(false);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, IWebHostEnvironment webHostEnvironment, Exception ex)
        {
            string errorResult;

            if (webHostEnvironment.IsDevelopment())
            {
                errorResult = JsonSerializer.Serialize(new { ex.Message, InnerException = ex.InnerException?.Message, ex.StackTrace });
            }
            else
            {
                string message = "An error occured. Please contact the SM.";
                errorResult = JsonSerializer.Serialize(new { message });
            }
            
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";
            return httpContext.Response.WriteAsync(errorResult);
        }
    }
}
