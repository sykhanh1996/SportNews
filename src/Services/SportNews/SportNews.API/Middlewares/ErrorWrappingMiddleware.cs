﻿using SportNews.Shared.SeedWork;
using System.Text.Json;

namespace SportNews.API.Middlewares
{
    public class ErrorWrappingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorWrappingMiddleware> _logger;

        public ErrorWrappingMiddleware(RequestDelegate next, ILogger<ErrorWrappingMiddleware> logger)
        {
            _next = next;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            string errorMsg = string.Empty;
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                errorMsg = ex.Message;
                context.Response.StatusCode = 500;
            }

            if (!context.Response.HasStarted && context.Response.StatusCode != 204)
            {
                context.Response.ContentType = "application/json";

                var response = new ApiErrorResult<bool>(500, errorMsg);

                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
