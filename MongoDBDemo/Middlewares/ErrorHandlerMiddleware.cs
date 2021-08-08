using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MongoDBDemo.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBDemo.Middlewares
{
    public class ErrorHandlerMiddleware
    {
       
        private readonly RequestDelegate _next;
        private readonly bool _includeDetails;

        public ErrorHandlerMiddleware(RequestDelegate next, IWebHostEnvironment webHostEnvironment)
        {
            _next = next;
            _includeDetails = webHostEnvironment.EnvironmentName=="Development";
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception exception)
            {
                if (exception != null)
                {
                    var errorMessage = _includeDetails ? $"An error occured: {exception.Message}" : "An error occured";
                    var details = _includeDetails ? exception.ToString() : null;

                    var result = new OperationResult<string>(details, false, errorMessage);
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsJsonAsync(result);
                }
            }
        }
    }
}
