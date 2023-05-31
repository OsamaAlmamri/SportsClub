using Microsoft.AspNetCore.Diagnostics;
using SportsClub.Models;
using System.Net;

namespace SportsClub.Extensions;

public static class ExceptionMiddlewareExtension
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
    {
        app.UseExceptionHandler(
            appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(
                            new ResponseModel()
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = "Internal Server Error."
                            }.ToString());
                    }
                });
            });
    }
}
