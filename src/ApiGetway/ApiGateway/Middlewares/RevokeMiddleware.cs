using Kros.Caching;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using System.IdentityModel.Tokens.Jwt;
using static Azure.Core.HttpHeader;
using static System.Net.Mime.MediaTypeNames;

namespace ApiGateway.Middlewares
{
    public class RevokeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDistributedCache _cache;
        public RevokeMiddleware(RequestDelegate next, IDistributedCache cache)
        {
            _next = next;
            _cache = cache;
        }
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers.FirstOrDefault(f => f.Key == "Authorization");
            if (token.Key != null)
            {
                var tokenValue = token.Value.ToString().Split(" ")[1];
                var cachedData = await _cache.GetStringAsync($"Token_{tokenValue}");
                if (cachedData is not null)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            }
            await _next(context);

        }
    }
}
