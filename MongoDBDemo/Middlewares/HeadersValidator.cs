using Microsoft.AspNetCore.Http;
using MongoDBDemo.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBDemo.Middlewares
{
    public class HeadersValidator
    {
        private readonly Dictionary<string, string> _headersDictionary = new Dictionary<string, string>
        {
            { "Company","Flairestech" },
            { "Account","Open" },
        };
        private readonly RequestDelegate _next;
        public HeadersValidator(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var isRequestValid = true;
            var missingHeaders = new List<string>();
            foreach (var item in _headersDictionary)
            {
                isRequestValid = context.Request.Headers.Keys.Contains(item.Key) && context.Request.Headers[item.Key] == item.Value;
                if (isRequestValid == false)
                {
                    missingHeaders.Add(item.Key);
                } 

            }
            
            isRequestValid = true;

            if (isRequestValid==false)
            {
                context.Response.StatusCode = 400; //Bad Request                
                var errorMessage = $"{string.Join(",", missingHeaders)} headers are missing";
                await context.Response.WriteAsJsonAsync(new OperationResult<string>(errorMessage));
                return;
            }
           
            await _next.Invoke(context);
        }
    }
}
